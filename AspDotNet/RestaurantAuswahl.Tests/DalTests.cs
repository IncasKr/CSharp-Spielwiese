using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantAsuwahl.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace RestaurantAsuwahl.Tests
{    
    [TestClass]
    public class DalTests
    {
        private IDal dal;
        
        [TestInitialize]
        public void Init_AvantChaqueTest()
        {
            IDatabaseInitializer<RADbContext> init = new DropCreateDatabaseAlways<RADbContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new RADbContext());

            dal = new Dal();
        }

        [TestCleanup]
        public void ApresChaqueTest()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void CreerRestaurant_AvecUnNouveauRestaurant_ObtientTousLesRestaurantsRenvoitBienLeRestaurant()
        {
            dal.CreateRestaurant("La bonne fourchette", "0102030405");
            List<Restaurant> restos = dal.GetAllRestaurants();

            Assert.IsNotNull(restos);
            Assert.AreEqual(1, restos.Count);
            Assert.AreEqual("La bonne fourchette", restos[0].Name);
            Assert.AreEqual("0102030405", restos[0].Telephone);
        }

        [TestMethod]
        public void ModifierRestaurant_CreationDUnNouveauRestaurantEtChangementNomEtTelephone_LaModificationEstCorrecteApresRechargement()
        {
            dal.CreateRestaurant("La bonne fourchette", "0102030405");
            dal.EditRestaurant(1, "La bonne cuillère", null);

            List<Restaurant> restos = dal.GetAllRestaurants();
            Assert.IsNotNull(restos);
            Assert.AreEqual(1, restos.Count);
            Assert.AreEqual("La bonne cuillère", restos[0].Name);
            Assert.IsNull(restos[0].Telephone);
        }

        [TestMethod]
        public void RestaurantExiste_AvecCreationDunRestauraunt_RenvoiQuilExiste()
        {
            dal.CreateRestaurant("La bonne fourchette", "0102030405");

            bool existe = dal.RestaurantExists("La bonne fourchette");

            Assert.IsTrue(existe);
        }

        [TestMethod]
        public void RestaurantExiste_AvecRestaurauntInexistant_RenvoiQuilExiste()
        {
            bool existe = dal.RestaurantExists("La bonne fourchette");

            Assert.IsFalse(existe);
        }

        [TestMethod]
        public void ObtenirUtilisateur_UtilisateurInexistant_RetourneNull()
        {
            User utilisateur = dal.GetUser(1);
            Assert.IsNull(utilisateur);
        }

        [TestMethod]
        public void ObtenirUtilisateur_IdNonNumerique_RetourneNull()
        {
            User utilisateur = dal.GetUser("abc");
            Assert.IsNull(utilisateur);
        }

        [TestMethod]
        public void AjouterUtilisateur_NouvelUtilisateurEtRecuperation_LutilisateurEstBienRecupere()
        {
            dal.AddUser("Nouvel utilisateur", "12345");

            User utilisateur = dal.GetUser(1);

            Assert.IsNotNull(utilisateur);
            Assert.AreEqual("Nouvel utilisateur", utilisateur.FirstName);

            utilisateur = dal.GetUser("1");

            Assert.IsNotNull(utilisateur);
            Assert.AreEqual("Nouvel utilisateur", utilisateur.FirstName);
        }

        [TestMethod]
        public void Authentifier_LoginMdpOk_AuthentificationOK()
        {
            dal.AddUser("Nouvel utilisateur", "12345");

            User utilisateur = dal.Authenticate("Nouvel utilisateur", "12345");

            Assert.IsNotNull(utilisateur);
            Assert.AreEqual("Nouvel utilisateur", utilisateur.FirstName);
        }

        [TestMethod]
        public void Authentifier_LoginOkMdpKo_AuthentificationKO()
        {
            dal.AddUser("Nouvel utilisateur", "12345");
            User utilisateur = dal.Authenticate("Nouvel utilisateur", "0");

            Assert.IsNull(utilisateur);
        }

        [TestMethod]
        public void Authentifier_LoginKoMdpOk_AuthentificationKO()
        {
            dal.AddUser("Nouvel utilisateur", "12345");
            User utilisateur = dal.Authenticate("Nouvel", "12345");

            Assert.IsNull(utilisateur);
        }

        [TestMethod]
        public void Authentifier_LoginMdpKo_AuthentificationKO()
        {
            User utilisateur = dal.Authenticate("Nouvel utilisateur", "12345");

            Assert.IsNull(utilisateur);
        }

        [TestMethod]
        public void ADejaVote_AvecIdNonNumerique_RetourneFalse()
        {
            bool pasVote = dal.AlreadyVoted(1, "abc");

            Assert.IsFalse(pasVote);
        }

        [TestMethod]
        public void ADejaVote_UtilisateurNAPasVote_RetourneFalse()
        {
            int idSondage = dal.CreateSurvey();
            int idUtilisateur = dal.AddUser("Nouvel utilisateur", "12345");

            bool pasVote = dal.AlreadyVoted(idSondage, idUtilisateur.ToString());

            Assert.IsFalse(pasVote);
        }

        [TestMethod]
        public void ADejaVote_UtilisateurAVote_RetourneTrue()
        {
            int idSondage = dal.CreateSurvey();
            int idUtilisateur = dal.AddUser("Nouvel utilisateur", "12345");
            dal.CreateRestaurant("La bonne fourchette", "0102030405");
            dal.AddVote(idSondage, 1, idUtilisateur);
            bool aVote = dal.AlreadyVoted(idSondage, idUtilisateur.ToString());

            Assert.IsTrue(aVote);
        }

        [TestMethod]
        public void ObtenirLesResultats_AvecQuelquesChoix_RetourneBienLesResultats()
        {
            int idSondage = dal.CreateSurvey();
            int idUtilisateur1 = dal.AddUser("Utilisateur1", "12345");
            int idUtilisateur2 = dal.AddUser("Utilisateur2", "12345");
            int idUtilisateur3 = dal.AddUser("Utilisateur3", "12345");

            dal.CreateRestaurant("Resto pinière", "0102030405");
            dal.CreateRestaurant("Resto pinambour", "0102030405");
            dal.CreateRestaurant("Resto mate", "0102030405");
            dal.CreateRestaurant("Resto ride", "0102030405");

            dal.AddVote(idSondage, 1, idUtilisateur1);
            dal.AddVote(idSondage, 3, idUtilisateur1);
            dal.AddVote(idSondage, 4, idUtilisateur1);
            dal.AddVote(idSondage, 1, idUtilisateur2);
            dal.AddVote(idSondage, 1, idUtilisateur3);
            dal.AddVote(idSondage, 3, idUtilisateur3);

            List<Results> resultats = dal.GetResults(idSondage);

            Assert.AreEqual(3, resultats[0].NumberOfVotes);
            Assert.AreEqual("Resto pinière", resultats[0].Name);
            Assert.AreEqual("0102030405", resultats[0].Telephone);
            Assert.AreEqual(2, resultats[1].NumberOfVotes);
            Assert.AreEqual("Resto mate", resultats[1].Name);
            Assert.AreEqual("0102030405", resultats[1].Telephone);
            Assert.AreEqual(1, resultats[2].NumberOfVotes);
            Assert.AreEqual("Resto ride", resultats[2].Name);
            Assert.AreEqual("0102030405", resultats[2].Telephone);
        }

        [TestMethod]
        public void ObtenirLesResultats_AvecDeuxSondages_RetourneBienLesBonsResultats()
        {
            int idSondage1 = dal.CreateSurvey();
            int idUtilisateur1 = dal.AddUser("Utilisateur1", "12345");
            int idUtilisateur2 = dal.AddUser("Utilisateur2", "12345");
            int idUtilisateur3 = dal.AddUser("Utilisateur3", "12345");
            dal.CreateRestaurant("Resto pinière", "0102030405");
            dal.CreateRestaurant("Resto pinambour", "0102030405");
            dal.CreateRestaurant("Resto mate", "0102030405");
            dal.CreateRestaurant("Resto ride", "0102030405");
            dal.AddVote(idSondage1, 1, idUtilisateur1);
            dal.AddVote(idSondage1, 3, idUtilisateur1);
            dal.AddVote(idSondage1, 4, idUtilisateur1);
            dal.AddVote(idSondage1, 1, idUtilisateur2);
            dal.AddVote(idSondage1, 1, idUtilisateur3);
            dal.AddVote(idSondage1, 3, idUtilisateur3);

            int idSondage2 = dal.CreateSurvey();
            dal.AddVote(idSondage2, 2, idUtilisateur1);
            dal.AddVote(idSondage2, 3, idUtilisateur1);
            dal.AddVote(idSondage2, 1, idUtilisateur2);
            dal.AddVote(idSondage2, 4, idUtilisateur3);
            dal.AddVote(idSondage2, 3, idUtilisateur3);

            List<Results> resultats1 = dal.GetResults(idSondage1);
            List<Results> resultats2 = dal.GetResults(idSondage2);

            Assert.AreEqual(3, resultats1[0].NumberOfVotes);
            Assert.AreEqual("Resto pinière", resultats1[0].Name);
            Assert.AreEqual("0102030405", resultats1[0].Telephone);
            Assert.AreEqual(2, resultats1[1].NumberOfVotes);
            Assert.AreEqual("Resto mate", resultats1[1].Name);
            Assert.AreEqual("0102030405", resultats1[1].Telephone);
            Assert.AreEqual(1, resultats1[2].NumberOfVotes);
            Assert.AreEqual("Resto ride", resultats1[2].Name);
            Assert.AreEqual("0102030405", resultats1[2].Telephone);

            Assert.AreEqual(1, resultats2[0].NumberOfVotes);
            Assert.AreEqual("Resto pinambour", resultats2[0].Name);
            Assert.AreEqual("0102030405", resultats2[0].Telephone);
            Assert.AreEqual(2, resultats2[1].NumberOfVotes);
            Assert.AreEqual("Resto mate", resultats2[1].Name);
            Assert.AreEqual("0102030405", resultats2[1].Telephone);
            Assert.AreEqual(1, resultats2[2].NumberOfVotes);
            Assert.AreEqual("Resto pinière", resultats2[2].Name);
            Assert.AreEqual("0102030405", resultats2[2].Telephone);
        }
    }
}
