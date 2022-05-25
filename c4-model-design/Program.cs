﻿using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            Travel();
        }

        static void Travel()
        {
            const long workspaceId = 74389;
            const string apiKey = "afcadd4d-8689-41ba-8921-8684ccb8f581";
            const string apiSecret = "25fb17e7-8066-4ea3-8106-ebc6476f4a66";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);
            Workspace workspace = new Workspace("Software Design & Patterns - C4 Model - LifeTravel", "LifeTravel");
            ViewSet viewSet = workspace.Views;
            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem travelSystem = model.AddSoftwareSystem("LifeTravel Application", "Permite la comunicacion entre usarios de la plataforma y maneja peticiones.");
            SoftwareSystem firebaseAPI = model.AddSoftwareSystem("Google Firebase", "Plataforma que ofrece una API para la autenticacion de usuarios y almacenamiento de datos con Firestore.");
            SoftwareSystem paypalAPI = model.AddSoftwareSystem("Paypal", "Plataforma que ofrece una API para integrar pagos dentro de la aplicacion por medio de Paypal");
            Person viajero = model.AddPerson("Usuario Viajero", "Usuario que desea establecer una conexión con el sistema.");
            Person agencia = model.AddPerson("Agencia de Viajes", "Usuario que desea establecer una conexión con el sistema.");

            viajero.Uses(travelSystem, "Realiza consultas para adquirir un paquete de viajes");
            agencia.Uses(travelSystem, "Realiza consultas para ingresar sus paquetes de viajes");
            travelSystem.Uses(firebaseAPI, "Usa la API de Firebase");
            travelSystem.Uses(paypalAPI, "Usa la API de Paypal");


            SystemContextView contextView = viewSet.CreateSystemContextView(travelSystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags
            viajero.AddTags("Viajero");
            agencia.AddTags("Agencia");
            travelSystem.AddTags("SistemaViajes");
            firebaseAPI.AddTags("Google Firebase");
            paypalAPI.AddTags("Paypal");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Viajero") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Agencia") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("SistemaViajes") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Google Firebase") { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("Paypal") { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });

            // 2. Diagrama de Contenedores
            Container mobileApplication = travelSystem.AddContainer("Mobile App", "Permite realizar las operaciones de los usuarios.", "React Native");
            Container landingPage = travelSystem.AddContainer("Landing Page", "Presenta el producto e incentiva a los usuarios a conocer la aplicación móvil", "ReactJS");
            Container webApplication = travelSystem.AddContainer("Web App", "Permite a los usuarios interactuar con la UI y sus funcionalidades.", "Angular Web");
            Container apiRest = travelSystem.AddContainer("API Rest", "Proporciona las funcionalidades mediante un API", "NodeJS (NestJS) port 8080");
            Container database = travelSystem.AddContainer("Database", "", "MYSQL");
            Container tripPlanningContext = travelSystem.AddContainer("Trip Planning Context", "Bounded Context del Microservicio de Planificación de Viajes", "NodeJS (NestJS)");
            Container newServiceContext = travelSystem.AddContainer("Service Context", "Bounded Context del Microservicio de Servicios", "NodeJS (NestJS)");
            Container reservationContext = travelSystem.AddContainer("Reservation Context", "Bounded Context del Microservicio de Reservas de Viajes", "NodeJS (NestJS)");
            Container authenticationContext = travelSystem.AddContainer("Authentication Context", "Bounded Context del Microservicio de Autenticación", "NodeJS (NestJS)");
            Container paymentContext = travelSystem.AddContainer("Payment Context", "Bounded Context del Microservicio de Pagos", "NodeJS (NestJS)");
            Container storageContext = travelSystem.AddContainer("Storage Context", "Bounded Context de Almacenamiento", "NodeJS (NestJS)");
            Container cardContext = travelSystem.AddContainer("Card Context", "Bounded Context para Tarjetas de debito y credito", "NodeJS (NestJS)");


            viajero.Uses(landingPage, "Consulta/Visita");
            viajero.Uses(mobileApplication, "Consulta/Visita");
            viajero.Uses(webApplication, "Consulta/Visita");
            agencia.Uses(landingPage, "Consulta/Visita");
            agencia.Uses(mobileApplication, "Consulta/Visita");
            agencia.Uses(webApplication, "Consulta/Visita");

            landingPage.Uses(mobileApplication, "Dirige a los usuarios a la aplicación móvil");
            webApplication.Uses(apiRest, "Realiza peticiones a la API", "JSON/HTTPS");
            mobileApplication.Uses(apiRest, "Realiza peticiones a la API", "JSON/HTTPS");

            apiRest.Uses(tripPlanningContext, "", "");
            apiRest.Uses(newServiceContext, "", "");
            apiRest.Uses(reservationContext, "", "");
            apiRest.Uses(authenticationContext, "", "");
            apiRest.Uses(paymentContext, "", "");
            apiRest.Uses(cardContext, "", "");

            tripPlanningContext.Uses(database, "", "JDBC");
            newServiceContext.Uses(database, "", "JDBC");
            reservationContext.Uses(database, "", "JDBC");
            paymentContext.Uses(database, "", "JDBC");
            storageContext.Uses(database, "", "JDBC");
            cardContext.Uses(database, "", "JDBC");

            paymentContext.Uses(paypalAPI,"API Request", "JSON/HTTPS");
            authenticationContext.Uses(firebaseAPI, "API Request", "JSON/HTTPS");
            storageContext.Uses(firebaseAPI, "API Request", "JSON/HTTPS");
            cardContext.Uses(firebaseAPI, "API Request", "JSON/HTTPS");
            cardContext.Uses(paypalAPI, "API Request", "JSON/HTTPS");


            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            database.AddTags("Database");
            tripPlanningContext.AddTags("TripPlanningContext");
            newServiceContext.AddTags("NewServiceContext");
            reservationContext.AddTags("ReservationContext");
            authenticationContext.AddTags("AuthenticationContext");
            paymentContext.AddTags("PaymentContext");
            storageContext.AddTags("StorageContext");
            cardContext.AddTags("CardContext");

            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("TripPlanningContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("NewServiceContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("ReservationContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("AuthenticationContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("StorageContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("CardContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(travelSystem, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            //3. Diagrama de Componentes
            //Bounded Context Trip Travel
            Component tripPlanDomainLayer = tripPlanningContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component tripPlanController = tripPlanningContext.AddComponent("Trip Plan Controller", "REST API endpoints de planes.", "NodeJS (NestJS) REST Controller");
            Component tripPlanService = tripPlanningContext.AddComponent("Plan Application Service", "Provee métodos para los planes, pertenece a la capa Application de DDD", "NestJS Component");
            Component planRepository = tripPlanningContext.AddComponent("Plan Repository", "Información del plan", "NestJS Component");
            

            apiRest.Uses(tripPlanController,"JSON");
            tripPlanController.Uses(tripPlanService,"Use");
            tripPlanService.Uses(planRepository,"Use");
            tripPlanService.Uses(tripPlanDomainLayer,"");
            planRepository.Uses(database,"","JDBC");
            //tags
            tripPlanDomainLayer.AddTags("Component");
            tripPlanController.AddTags("Component");
            tripPlanService.AddTags("Component");
            planRepository.AddTags("Component");

            //style
            styles.Add(new ElementStyle("Component") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView planComponentView = viewSet.CreateComponentView(tripPlanningContext, "Plan Components", "Component Diagram");

            planComponentView.PaperSize = PaperSize.A4_Landscape;
            planComponentView.Add(mobileApplication);
            planComponentView.Add(webApplication);
            planComponentView.Add(apiRest);
            planComponentView.Add(database);
            planComponentView.AddAllComponents();



            //3. Diagrama de Componentes
            //Bounded Context Authentication
            Component authenticationDomainLayer = authenticationContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component authenticationController = authenticationContext.AddComponent("Authentication Controller", "REST API endpoints de autenticacion.", "NodeJS (NestJS) REST Controller");
            Component authenticationService = authenticationContext.AddComponent("Authentication Application Service", "Provee métodos para al autenticación, pertenece a la capa Application de DDD", "NestJS Component");

            apiRest.Uses(authenticationController, "JSON");
            authenticationController.Uses(authenticationService, "Use");
            authenticationService.Uses(authenticationDomainLayer, "");
            authenticationService.Uses(firebaseAPI, "", "HTTPS/JSON");

            //tags
            tripPlanDomainLayer.AddTags("Component");
            tripPlanController.AddTags("Component");
            tripPlanService.AddTags("Component");

            //style
            //styles.Add(new ElementStyle("Component") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView authenticationComponentView = viewSet.CreateComponentView(authenticationContext, "Autentication Components", " Component Diagram");

            authenticationComponentView.PaperSize = PaperSize.A4_Landscape;
            authenticationComponentView.Add(mobileApplication);
            authenticationComponentView.Add(webApplication);
            authenticationComponentView.Add(apiRest);
            authenticationComponentView.Add(firebaseAPI);
            authenticationComponentView.AddAllComponents();


            //3. Diagrama de Componentes
            //Bounded Context Card
            Component cardDomainLayer = cardContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component cardController = cardContext.AddComponent("Card Controller", "REST API endpoints de card.", "NodeJS (NestJS) REST Controller");
            Component cardService = cardContext.AddComponent("Card Application Service", "Provee métodos para al card, pertenece a la capa Application de DDD", "NestJS Component");

            apiRest.Uses(cardController, "JSON");
            cardController.Uses(cardService, "Use");
            cardService.Uses(cardDomainLayer, "");
            cardService.Uses(firebaseAPI, "", "HTTPS/JSON");
            cardService.Uses(paypalAPI, "", "HTTPS/JSON");

            //tags
            tripPlanDomainLayer.AddTags("Component");
            tripPlanController.AddTags("Component");
            tripPlanService.AddTags("Component");

            //style
            //styles.Add(new ElementStyle("Component") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView cardComponentView = viewSet.CreateComponentView(cardContext, "Card Components", " Component Diagram");

            cardComponentView.PaperSize = PaperSize.A4_Landscape;
            cardComponentView.Add(mobileApplication);
            cardComponentView.Add(webApplication);
            cardComponentView.Add(apiRest);
            cardComponentView.Add(firebaseAPI);
            cardComponentView.Add(paypalAPI);
            cardComponentView.AddAllComponents();

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}