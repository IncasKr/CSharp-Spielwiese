using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace RDPLoader
{
    #region Structures

    enum RDPType
    {
        b, //encrypted byte for the password
        i, // integer
        s // string
    }

    enum RDPFieldType
    {
        name,
        type,
        value
    }

    /// <summary>
    /// Structur of RDPFile's field.
    /// </summary>
    struct RDPField
    {
        private string Name;
        private RDPType Type;
        private string Value;

        /// <summary>
        /// Constructor of RDPFile's field.
        /// </summary>
        /// <param name="name">Name of RDP File's field</param>
        /// <param name="type">Type of RDP File's field</param>
        /// <param name="value">Value of RDP File's field</param>
        public RDPField(string name, RDPType type = RDPType.s, string value = "")
        {
            Name = name;
            Type = type;
            Value = value;
            SetValue(value);
        }

        public static bool operator ==(RDPField field1, RDPField field2)
        {
            if ((field1.Name != field2.Name) || (field1.Type != field2.Type) || (field1.Value != field2.Value))
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(RDPField field1, RDPField field2)
        {
            if (field1.Name == field2.Name)
            {
                return false;
            }
            return true;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetValue()
        {
            return Value;
        }
        
        /// <summary>
        /// Gets values of RDPField.
        /// </summary>
        /// <returns>string in the form "name:type:value".</returns>
        public override string ToString() => $"{Name}:{Type}:{Value}";

        /// <summary>
        /// Sets the value of field
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            switch (Type)
            {
                case RDPType.i:
                    try
                    {
                        Value = int.Parse(value).ToString();
                    }
                    catch (Exception)
                    {
                        Value = "";
                    }
                    break;
                default:
                    Value = value;
                    break;
            }
        }
    }


    #endregion
    /// <summary>
    /// 
    /// </summary>
    class RDPDocument
    {
        #region Imports

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string path, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder shortPath, int shortPathLength);

        #endregion

        #region Properties

        private static DPCryptor cryptor = new DPCryptor();

        private string _filename = $"{GetRDPDir()}default.RDP";
        /// <summary>
        /// Filename of this document with extension.
        /// </summary>
        public string Filename
        {
            get { return _filename; }
            set
            {
                if (_filename != value && !String.IsNullOrWhiteSpace(value))
                {
                    if (_filename.Contains(".") && _filename.Substring(_filename.LastIndexOf('.')).ToUpper() == ".RDP")
                    {
                        _filename = $"{GetRDPDir()}{value}";
                    }
                    else
                    {
                        _filename = $"{GetRDPDir()}{value}.RDP";
                    }
                }
            }
        }

        #endregion

        #region Fields

        private RDPField _username;
        /// <summary>
        /// Specifies the name of the user account that will be used to log on to the remote computer.
        /// </summary>
        public RDPField Username
        {
            get { return _username; }
            private set
            {
                if (_username != value)
                {
                    _username = value;
                }
            }
        }

        /// <summary>
        /// The user password
        /// </summary>
        private RDPField Password;

        /// <summary>
        /// Specifies the name or IP address (and optional port) of the remote computer that you want to connect to.
        /// </summary>
        private RDPField Host;

        /// <summary>
        /// Defines an alternate default port for the Remote Desktop connection.
        /// </summary>
        private RDPField Port;

        /// <summary>
        /// Connect to the administrative session of the remote computer.
        /// </summary>
        private RDPField Admin;

        /// <summary>
        /// The height (in pixels) of the remote session desktop.
        /// </summary>
        private RDPField Height;

        /// <summary>
        /// The width (in pixels) of the remote session desktop.
        /// </summary>
        private RDPField Width;

        /// <summary>
        /// Determines the color depth (in bits) on the remote computer when you connect.
        /// </summary>
        private RDPField Bit;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of RDP Document
        /// </summary>
        public RDPDocument()
        {
            Init();
        }

        /// <summary>
        /// Constructor of RDP Document with parameters
        /// </summary>
        /// <param name="user">username</param>
        /// <param name="pwd">password</param>
        /// <param name="host">host</param>
        /// <param name="port">port</param>
        /// <param name="admin">admin</param>
        /// <param name="height">height</param>
        /// <param name="width">width</param>
        /// <param name="colorBit">color bit</param>
        public RDPDocument(string user, string pwd, string host = "127.0.0.1", int port = 3389, bool admin = true, int height = 600, int width = 800, int colorBit = 32)
        {
            Username = new RDPField("username", RDPType.s, user);
            Password = new RDPField("password 51", RDPType.b, cryptor.Encrypt(pwd));
            Host = new RDPField("full address", RDPType.s, host);
            Port = new RDPField("server port", RDPType.i, port.ToString());
            Admin = new RDPField("administrative session", RDPType.i, admin ? "1" : "0");
            Height = new RDPField("desktopheight", RDPType.i, height.ToString());
            Width = new RDPField("desktopwidth", RDPType.i, width.ToString());
            Bit = new RDPField("session bpp", RDPType.i, colorBit.ToString());
        }

        /// <summary>
        /// Initializes the RDP document with the default values
        /// </summary>
        public void Init()
        {
            Username = new RDPField("username", RDPType.s, "");
            Password = new RDPField("password 51", RDPType.b, cryptor.Encrypt(""));
            Host = new RDPField("full address", RDPType.s, "");
            Port = new RDPField("server port", RDPType.i, "3389");
            Admin = new RDPField("administrative session", RDPType.i, "0");
            Height = new RDPField("desktopheight", RDPType.i, "600");
            Width = new RDPField("desktopwidth", RDPType.i, "800");
            Bit = new RDPField("session bpp", RDPType.i, "32");
        }

        #endregion


        #region Methodes

        /// <summary>
        /// finds a FieldType's value in the string.
        /// </summary>
        /// <param name="fieldType">The Filetype to search</param>
        /// <param name="line">The gived string</param>
        /// <returns>The value of FieldType contained in the string</returns>
        public string Find(RDPFieldType fieldType, string line)
        {
            if (IsRDPLine(line))
            {
                return line.Split(':')[(int)fieldType];
            }
            else
            {
                return "";
            }
        }

        private static string GetRDPDir()
        {
            try
            {
                if (Directory.Exists($"{Directory.GetCurrentDirectory()}\\RDPProfiles"))
                {
                    return GetShortPathName($"{Directory.GetCurrentDirectory()}\\RDPProfiles\\");
                }
                else
                {
                    return GetShortPathName(Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\RDPProfiles").FullName) + "\\";
                }
            }
            catch (IOException)
            {
                return null;
            }
        }

        private static string GetShortPathName(string longPath)
        {
            StringBuilder shortPath = new StringBuilder(255);
            GetShortPathName(longPath, shortPath, shortPath.Capacity);
            return shortPath.ToString();
        }

        /// <summary>
        /// Gets the string of RDPField in the form "name:type:value"
        /// </summary>
        /// <param name="field">Represents the line in the form "name:type:value"</param>
        /// <returns>a string of value</returns>
        public string GetFieldValue(RDPField field)
        {
            return field.ToString();
        }

        /// <summary>
        /// Checks if the entering file is a valid RDP file.
        /// </summary>
        /// <param name="filename">The path of file</param>
        /// <returns>True if it's a valid RDP file, otherwise false.</returns>
        public bool IsRDPFile(string filename)
        {
            bool isValid = true;

            if (String.IsNullOrWhiteSpace(filename) && filename.Substring(filename.LastIndexOf('.')).ToUpper() != ".RDP")
            {
                return false;
            }
            
            try
            {
                if (!File.Exists(filename))
                {
                    return false;
                }
                using (FileStream fs = File.OpenRead(filename))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string line;
                    while (fs.Read(b, 0, b.Length) > 0 && isValid)
                    {
                        line = temp.GetString(b);
                        if (!IsRDPLine(line))
                        {
                            isValid = false;
                        }
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on load: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Checks if the entering string is a valid RDP line.
        /// </summary>
        /// <param name="line">The string to check</param>
        /// <returns>True if it's a valid RDP line, otherwise false.</returns>
        public bool IsRDPLine(string line)
        {
            return (line.Split(':').Length == 3) ? true : false;
        }

        /// <summary>
        /// Sets only an integer value of RDPField. 
        /// </summary>
        /// <param name="field">RDPField object</param>
        /// <param name="value">value to set</param>
        public void SetFieldValue(RDPField field, int value)
        {
            field.SetValue(value.ToString());
        }

        /// <summary>
        /// Sets only a string value of RDPField.
        /// </summary>
        /// <param name="field">RDPField object</param>
        /// <param name="value">value to set</param>
        public void SetFieldValue(RDPField field, string value)
        {
            if (field.GetName() == "administrative session")
            {
                if (field.GetValue() == "1")
                {
                    cryptor.CurrentStore = Store.USE_MACHINE_STORE;
                }
                else
                {
                    cryptor.CurrentStore = Store.USE_USER_STORE;
                }
            }

            if (field.GetName() == "password 51")
            {
                field.SetValue(cryptor.Encrypt(value));
            }
            else
            {
                field.SetValue(value);
            }
        }

        #endregion


        #region IO Methodes

        /// <summary>
        /// Compares a RDP file with the current RDP document.
        /// </summary>
        /// <param name="filename">The path of file to compare</param>
        /// <returns>True if the file is same as the current document, otherwise false.</returns>
        public bool Compare(string filename)
        {
            bool isEqual = true;

            try
            {
                if (!File.Exists(filename) || (File.Exists(filename) && !IsRDPFile(filename)))
                {
                    return false;
                }
                using (FileStream fs = File.OpenRead(filename))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string line;
                    while (fs.Read(b, 0, b.Length) > 0 && isEqual)
                    {
                        line = temp.GetString(b);
                        if ((Find(RDPFieldType.name, line) != Admin.GetName()) ||
                            (Find(RDPFieldType.name, line) != Height.GetName()) ||
                            (Find(RDPFieldType.name, line) != Width.GetName()) ||
                            (Find(RDPFieldType.name, line) != Host.GetName()) ||
                            (Find(RDPFieldType.name, line) != Password.GetName()) ||
                            (Find(RDPFieldType.name, line) != Port.GetName()) ||
                            (Find(RDPFieldType.name, line) != Bit.GetName()) ||
                            (Find(RDPFieldType.name, line) != Username.GetName()))
                        {
                            isEqual = false;
                        }                                             
                    }
                }
                return isEqual;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on load: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Loads a RDP file into a RDP document.
        /// </summary>
        /// <param name="filename">The path of file</param>
        /// <returns>True if the file is successful loaded, otherwise false.</returns>
        public bool Load(string filename)
        {
            try
            {
                if (!File.Exists(filename) || (File.Exists(filename) && !IsRDPFile(filename)))
                {
                    return false;
                }

                using (FileStream fs = File.OpenRead(filename))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string line;
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        line = temp.GetString(b);
                        if (Find(RDPFieldType.name, line) == Admin.GetName())
                        {
                            SetFieldValue(Admin, Find(RDPFieldType.value, line));
                        }
                        else if (Find(RDPFieldType.name, line) == Height.GetName())
                        {
                            SetFieldValue(Height, Find(RDPFieldType.value, line));
                        }
                        else if (Find(RDPFieldType.name, line) == Width.GetName())
                        {
                            SetFieldValue(Width, Find(RDPFieldType.value, line));
                        }
                        else if (Find(RDPFieldType.name, line) == Host.GetName())
                        {
                            SetFieldValue(Host, Find(RDPFieldType.value, line));
                        }
                        else if (Find(RDPFieldType.name, line) == Password.GetName())
                        {
                            SetFieldValue(Password, Find(RDPFieldType.value, line));
                        }
                        else if (Find(RDPFieldType.name, line) == Port.GetName())
                        {
                            SetFieldValue(Port, Find(RDPFieldType.value, line));
                        }
                        else if (Find(RDPFieldType.name, line) == Bit.GetName())
                        {
                            SetFieldValue(Bit, Find(RDPFieldType.value, line));
                        }
                        else if (Find(RDPFieldType.name, line) == Username.GetName())
                        {
                            SetFieldValue(Username, Find(RDPFieldType.value, line));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on load: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Saves a RDP document into a RDP file.
        /// </summary>
        /// <param name="filename">The path of file</param>
        /// <returns>True if the file is successful saved, otherwise false.</returns>
        public bool Save(string filename)
        {
            if (File.Exists(filename) && Compare(filename))
            {
                return false;
            }
            try
            {
                using (FileStream file = File.Create(filename))
                {
                    AddLine(file, $"{Admin.ToString()}\n");
                    AddLine(file, $"{Height.ToString()}\n");
                    AddLine(file, $"{Width.ToString()}\n");
                    AddLine(file, $"{Host.ToString()}\n");
                    AddLine(file, $"{Password.ToString()}\n");
                    AddLine(file, $"{Port.ToString()}\n");
                    AddLine(file, $"{Bit.ToString()}\n");
                    AddLine(file, $"{Username.ToString()}\n");
                    AddLine(file, $"screen mode id:i:1\n");
                    //AddLine(file, $"smart sizing:i:1\n");
                    //AddLine(file, $"enablesuperpan:i:0\n");
                    //AddLine(file, $"connect to console:i:1\n");
                    //AddLine(file, $"winposstr:s:0,3,0,0,{Width.GetValue()},{Height.GetValue()}\n");
                    //AddLine(file, $"use multimon:i:1");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on save: {ex.Message}");
                return false;
            }

        }

        private void AddLine(FileStream file, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            file.Write(info, 0, info.Length);
        }

        #endregion
    }
}
