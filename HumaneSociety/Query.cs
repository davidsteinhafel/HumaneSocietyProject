using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();

            return allStates;
        }

        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }

            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;

            // submit changes
            db.SubmitChanges();
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }


        //// TODO Items: ////

        // TODO: Allow any of the CRUD operations to occur here
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {

            switch (crudOperation)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    break;
                case "read":
                    var readEmployee = db.Employees.Where(y => y.EmployeeNumber == employee.EmployeeNumber).SingleOrDefault();
                    Console.Write($" First Name: {readEmployee.FirstName}, Last Name: {readEmployee.LastName}, Password: {readEmployee.Password}, ID: {readEmployee.EmployeeId}, Number: {readEmployee.EmployeeNumber}, Email: {readEmployee.Email}");
                    Console.ReadLine();
                    break;
                case "update":
                    var employeeOnDb = db.Employees.Where(y => y.EmployeeNumber == employee.EmployeeNumber).SingleOrDefault();
                    employeeOnDb.FirstName = employee.FirstName;
                    employeeOnDb.LastName = employee.LastName;
                    employeeOnDb.Password = employee.Password;
                    employeeOnDb.UserName = employee.UserName;
                    employeeOnDb.EmployeeNumber = employee.EmployeeNumber;
                    employeeOnDb.EmployeeId = employee.EmployeeId;
                    employeeOnDb.Email = employee.Email;
                    db.SubmitChanges();

                    break;
                case "delete":
                    var deleteEmployeeOnDb = db.Employees.Where(y => y.EmployeeNumber == employee.EmployeeNumber).SingleOrDefault();
                    db.Employees.DeleteOnSubmit(deleteEmployeeOnDb);
                    db.SubmitChanges();
                    break;
            }
        }

        // -----------------------------------------TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
<<<<<<< HEAD
            
            if (animal == null)
=======
            //DID need check
            Animal addNewAnimal = db.Animals.Where(x => x.AnimalId == animal.AnimalId).FirstOrDefault();
            if (addNewAnimal == null)
            {
                throw new Exception();
            }
            else
>>>>>>> f84c690cb461a6a80ca02dd69b0c6a6fb128dfb6
            {
                throw new NullReferenceException("animal");
            }
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
            
        
        
        }

        internal static Animal GetAnimalByID(int id)
        {
            var animalOnDb = db.Animals.Where(a => a.AnimalId == id).FirstOrDefault();
            if (animalOnDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return animalOnDb;
            }
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            var animalOnDB = db.Animals.Where(a => a.AnimalId == animalId).SingleOrDefault();
            foreach (var items in updates) //query through database using animalId.  for each over a dictionary witha switch case (animal.key = animal.value) switch over key in dictionary

            {
                switch (items.Key)
                {
                    case 1:
                        var cata = db.Categories.Where(c => c.Name == items.Value).SingleOrDefault();
                        animalOnDB.CategoryId = cata.CategoryId;
                        break;
                    case 2:
                        animalOnDB.Name = items.Value;
                        break;
                    case 3:
                        var age = db.Animals.Where(a => a.Name == items.Value).SingleOrDefault();
                        animalOnDB.Age = age.Age;
                        break;
                    case 4:
                        animalOnDB.Demeanor = items.Value;
                        break;
                    case 5:
                        var kids = db.Animals.Where(k => k.Name == items.Value).SingleOrDefault();
                        animalOnDB.KidFriendly = kids.KidFriendly;
                        break;
                    case 6:
                        var pets = db.Animals.Where(p => p.Name == items.Value).SingleOrDefault();
                        animalOnDB.PetFriendly = pets.PetFriendly;
                        break;
                    case 7:
                        var wt = db.Animals.Where(w => w.Name == items.Value).SingleOrDefault();
                        animalOnDB.Weight = wt.Weight;
                        break;
                }
                string input = UserInterface.GetUserInput();
                if (input.ToLower() == "8" || input.ToLower() == "finished")
                {
                    Query.UpdateAnimal(animalId, updates);
                }
                else
                {
                    updates = UserInterface.EnterSearchCriteria(updates, input);
                    UpdateAnimal(animalId, updates);
                }

            }
        }

        internal static void RemoveAnimal(Animal animal)
        {

            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }

        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            throw new NotImplementedException();
        }

        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            var categoryOnDb = db.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
            return categoryOnDb.CategoryId;
        }

        internal static Room GetRoom(int animalId)
        {
            var roomOnDb = db.Rooms.Where(a => a.AnimalId == animalId).FirstOrDefault();
            if (roomOnDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return roomOnDb;
            }
        }

        internal static int GetDietPlanId(string dietPlanName)
        {
            var dietOnDb = db.DietPlans.Where(d => d.Name == dietPlanName).FirstOrDefault(); /*db.Employees.Where(e => e.UserName == userName).FirstOrDefault();*/
            return dietOnDb.DietPlanId;
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {

            var adoptOnDb = db.Adoptions.Where(x => x.AnimalId == animal.AnimalId).Where(y => y.ClientId == client.ClientId).SingleOrDefault();
            var adoptStatus = db.Animals.Where(a => a.AdoptionStatus == animal.AdoptionStatus).SingleOrDefault().ToString();
            if (adoptStatus.ToLower() == "open")
            {
                Console.WriteLine(adoptOnDb);
            }
            else
            {
                Console.WriteLine("Unfortunately the animal requested has already been reserved for adoption");
            }

        }
        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            var updateAdoptionOnDb = db.Adoptions.Where(a => a.AnimalId == adoption.AnimalId && a.ClientId == adoption.ClientId).SingleOrDefault();
            updateAdoptionOnDb.ApprovalStatus = isAdopted == true ? "APPROVED" : "PENDING";
            db.SubmitChanges();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            var removeAdoptionOnDb = db.Adoptions.Where(a => a.AnimalId == clientId).FirstOrDefault();
            db.Adoptions.DeleteOnSubmit(removeAdoptionOnDb);
            db.SubmitChanges();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var shotsOnDb = db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId);
            db.SubmitChanges();
            return shotsOnDb;
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            var shotUpdateOnDb = db.Shots.Where(s => s.AnimalShots == animal.AnimalShots).SingleOrDefault();
            shotUpdateOnDb.AnimalShots = animal.AnimalShots;
        }
    }
}