# EST-Mobile (EfficientSecureTransport-Mobile)

`EST-Mobile` er en mobilapplikation, som den er klar til at betjene den mobile arbejdsstyrke for den mest fiktiv virksomhed i Danmark.
**Nødhjælpsorganisation Danmark** er virksomheden, som står altid til at levere nødjælp i rigtig kort tid til verdens brændpunkter.

Med `EST-Mobile`, virksomhedens lastbilchauffører kan være i stand til at autentificer og foretag deres arbejde ved at se deres nuværende ruter, se en liste over alle ruter, samt opdatere deres placeringer på ruten.

# System Diagram

`EST-Mobile` er afghængig af [Efficient Secure Transport](https://github.com/nitram1337/EfficientSecureTransport) repository, så inden du kører `EST-Mobile`, sørg for at du henter og kører dette repository,
samt følg vejledningen til opsætningen som er længere ned ved sektionen: **Setup af enviroment**.

Her under vises et technical diagram for at forstå hvordan hele systemet er bygget på, og hvordan det fungerer fuldt ud.

![Technical diagram](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/EST_Mobileapp_Technical_Diagram.png)

# Architecture

Hele arkitekturen består af to dele:
  - En mobilapp bygget ved hjælp af frameworket: [Xamarin.Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/) som kan køres på Android og iOS.
  - En webapp bygget ved hjælp af frameworket: [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) med page-based model: [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio) 
  - Fire .NET Web-API [microservices](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/architect-microservice-container-applications/microservices-architecture) bygget ved hjælp af [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) 

`EST-Mobile` arkitekturen består af:
  - En [powershell](https://docs.microsoft.com/en-us/powershell/scripting/overview?view=powershell-7.2) script som sørg for at kører [adb](https://docs.microsoft.com/en-us/dual-screen/android/emulator/adb) komandoer, sådan at `EST-Mobile` kan nå [Identity microservice](https://github.com/nitram1337/EfficientSecureTransport/tree/master/EST.API.IdentityMS) og de 3 microservices via API Gateway.
  - En shared application code:  - **MobileApp**
  - En android platform render: - **MobileApp.Android**
  - En iOS platform render:     - **MobileApp.iOS**

Her under vises et applikation arkitektur diagram for at se mere kokrete.

![Application architecture diagram](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/EST_Mobileapp_Application_Architecture.png)

# Setup af enviroment

Vi anbefaler at du bruger den nyeste visual studio [Visual studio IDE](https://visualstudio.microsoft.com/vs/)
<br />

### 1. Benyt den nyeste opdatering af Visual Studio:
  > **Note:** (spring over step 1 hvis du har lige installeret den nyeste version af Visual Studio)
<br />

```
Windows Start | Visual Studio Installer | Update
```

<br />

### 2. Installer Xamarin
<br />

```
Windows Start | Visual Studio Installer | Modify | sæt flueben ved **Mobile development with .NET**
```

### 3. Aktivere Virtualisering
<br />

Virtualisering kan forbedre performance når man kører **adb**, derfor vi skal aktiver **Hyper-V** og **Windows Hypervisor Platform**
Man skal lige kontroller om *Hyper-V* er enabled i BIOS, og det gøre man ved at skrive *systeminfo* ind i **CMD**.
Der skal stå *Yes* ud for alle fire Hyper-V Requirements eller: *A hypervisor has been detected. Features required for Hyper-V will not be displayed.*

Der skal tjekkes om *Hyper-V* og *Windows Hypervisor Platform* er aktiveret.

```
Windows Start | Turn Windows features on or off | sæt flueben ved Hyper-V og Windows Hypervisor Platform
```

> **Note:** En genstart for at fuldføre aktivering, er påkrævet!
<br />

![Enabling Hyper-V and Windows Hypervisor Platform](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Setup%20af%20Enviroment/Hyper-V_config.png)

### 5. Tilføj workloads
<br />


