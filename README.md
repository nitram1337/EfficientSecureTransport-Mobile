# EST-Mobile (EfficientSecureTransport-Mobile)

`EST-Mobile` er en mobilapplikation, som den er klar til at betjene den mobile arbejdsstyrke for den mest fiktiv virksomhed i Danmark.
**Nødhjælpsorganisation Danmark** er virksomheden, som står altid til at levere nødjælp i rigtig kort tid til verdens brændpunkter.

Med `EST-Mobile`, virksomhedens lastbilchauffører kan være i stand til at autentificer og foretag deres arbejde ved at se deres nuværende ruter, se en liste over alle ruter, samt opdatere deres placeringer på ruten.

<br />


# System Diagram

`EST-Mobile` er afghængig af [Efficient Secure Transport](https://github.com/nitram1337/EfficientSecureTransport) repository, så inden du kører `EST-Mobile`, sørg for at du henter og kører dette repository,
samt følg `EST-Mobile` vejledningen til opsætningen som er længere ned ved sektionen: **Setup af enviroment**.

Her under vises et technical diagram for at forstå hvordan hele systemet er bygget på, og hvordan det fungerer fuldt ud.

![Technical diagram](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/EST_Mobileapp_Technical_Diagram.png)

<br />


# Architecture

Hele arkitekturen består af fire dele:
  - En mobilapp bygget ved hjælp af frameworket: [Xamarin.Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/) som kan køres på Android og iOS.
  - En webapp bygget ved hjælp af frameworket: [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) med page-based model: [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio) 
  - Fire .NET Web-API [microservices](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/architect-microservice-container-applications/microservices-architecture) bygget ved hjælp af [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
  - En .NET Web-API bygget ved hjælp af [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)

`EST-Mobile` arkitekturen består af:
  - En [powershell](https://docs.microsoft.com/en-us/powershell/scripting/overview?view=powershell-7.2) script som sørg for at kører [adb](https://docs.microsoft.com/en-us/dual-screen/android/emulator/adb) komandoer, sådan at `EST-Mobile` kan nå [Identity microservice](https://github.com/nitram1337/EfficientSecureTransport/tree/master/EST.API.IdentityMS) og de 3 microservices via API Gateway.
  - En shared application code:  - **MobileApp**
  - En android platform render: - **MobileApp.Android**
  - En iOS platform render:     - **MobileApp.iOS**

Her under vises et **applikation arkitektur diagram** for at se mere konkrete.

![Application architecture diagram](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/EST_Mobileapp_Application_Architecture.png)

For flere detaljer, henvises til [Xamarin.Forms](https://docs.microsoft.com/en-us/xamarin/get-started/what-is-xamarin-forms) artiklen.

<br />


# Nugets & .NET version
Under ses dokumentation for de nugets der er brugt med version, samt hvilken .NET version.

## MobileApp

.NET Standard 2.0

```xml
    <PackageReference Include="IdentityModel.OidcClient" Version="3.1.2" />
    <PackageReference Include="MonkeyCache.FileStore" Version="1.6.3" />
    <PackageReference Include="Xamarin.CommunityToolkit" Version="2.0.1" />
    <PackageReference Include="Xamarin.Forms" Version="5.0.0.2401" />  
    <PackageReference Include="Xamarin.Essentials" Version="1.7.2" />
```

## MobileApp.Android

```xml
    <PackageReference Include="MonkeyCache.FileStore">
          <Version>1.6.3</Version>
        </PackageReference>
        <PackageReference Include="Plugin.CurrentActivity">
          <Version>2.1.0.4</Version>
        </PackageReference>
        <PackageReference Include="Xamarin.CommunityToolkit">
          <Version>2.0.1</Version>
        </PackageReference>
        <PackageReference Include="Xamarin.Forms" Version="5.0.0.2401" />
        <PackageReference Include="Xamarin.Essentials" Version="1.7.2" />
```

## MobileApp.iOS

```xml
     <PackageReference Include="MonkeyCache.FileStore">
          <Version>1.6.3</Version>
        </PackageReference>
        <PackageReference Include="Xamarin.CommunityToolkit">
          <Version>2.0.1</Version>
        </PackageReference>
        <PackageReference Include="Xamarin.Forms" Version="5.0.0.2401" />
        <PackageReference Include="Xamarin.Essentials" Version="1.7.2" />
```

<br />


# Requirements
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) for at kompilere.
- [Xamarin add-ons](https://docs.microsoft.com/en-us/xamarin/get-started/installation/?pivots=windows) til Visual Studio (Se *punkt 1* fra **Setup** sektion)
- [Android SDK Tools](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/android-sdk?tabs=windows) (Se *punkt 3* fra *Setup* sektion)

<br />


# Setup af enviroment

Vi anbefaler at du bruger den nyeste [Visual studio IDE](https://visualstudio.microsoft.com/vs/)
<br />


### 1. Sørg for, at Xamarin add-ons er installeret
`Windows Start | Visual Studio Installer | Modify | flueben skal være ved Mobile development with .NET | Install while downloading`

<br />


### 2. Forbedre Android emulator performance med virtualisering
Virtualisering kan forbedre performance på **Android Emulator**, du kan bruge en af de følgende virtualisering teknologier: 
- **Hyper-V**
- Intel HAXM

Længere ned du kan finde en kort forklaring til hvordan man kan kontroller, om din PC understøtter Hyper-V og hvordan du aktiver det. <br />
Hvis du vil gerne vide mere, du kan læse [Hardware Acceleration for emulator performance (Hyper-V & HAXM)](https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/android-emulator/hardware-acceleration?pivots=windows) artiklen.

- Kontroller om, din PC hardware og software er kompatibel med *Hyper-V*  
`Windows Start | CMD | systeminfo`
Der skal stå *Yes* ud for alle fire Hyper-V Requirements eller: *A hypervisor has been detected. Features required for Hyper-V will not be displayed.*
- Kontroller om, *Hyper-V* og *Windows Hypervisor Platform* er aktiveret.
`Windows Start | Turn Windows features on or off | sæt flueben ved Hyper-V og Windows Hypervisor Platform`

> **Note:** En genstart for at fuldføre aktivering, er påkrævet!
<br />

![Enabling Hyper-V and Windows Hypervisor Platform](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Setup%20af%20Enviroment/Hyper-V_config.png)

<br />


### 3. Tilføj SDK og en mobil emulator
Åben **Android SDK Manager**<br />
`Visual Studio IDE | Continue without code | Tools | Android | Android SDK Manager`

> **Note:** Anbefaler ikke den helt nyeste Android version, for at undgå uforenelighed eller andre problemer.  <br />
> Den Android version, som `EST-Mobile` har brugt det er **Android 11.0 - R** <br />
> Hvis din PC har mere end 16 gb RAM, vi anbefaler også at du giver mere RAM til din emulator, ved at angive størelsen i MB ved **hw.ramSize** (se evt. billede lidt længere ned, hvor man tilføjer ny emulator enhed).

Vælg det ønskede *Android SDK Platform* og tilhørende *Intel x86 Atom System Image* - *Apply Changes* og foretag de nødvendige opdateringer.

![Choose Android SDK Platform](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Setup%20af%20Enviroment/Android_SDK_choosing_android.png)

<br />


Åben **Android Device Manager**<br />
`Visual Studio IDE | Continue without code | Tools | Android | Android Device Manager`

Prøv at starte emulatoren ved at klikke på *Start* knap.<br /> 
Hvis der er ingen mobil enhed, så kan du tilføj en ved at klikke på *New*.

![Open Android Device Manager](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Setup%20af%20Enviroment/Android_Device_Manager_starting_emulator.png)

<br />


På *New device* fane, `EST-Mobile` har brugt følgende enhed indstillinger:

|        Property        |      Values             |
|----------------|-------------------------------|
|**Base Device**|Pixel 5            |
|**Processor**        |x86_64            |
|**OS**         |Android 11.0 - API 30|
|**Google APIs**  | ✓ |
|**Google Play Store** | X |
|**hw.ramSize** | 1536 |

![Creating an Android Device Emulator](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Setup%20af%20Enviroment/Android_Device_Manager_creating_emulator.png)

# Run

### 1. Start emulatoren
`Visual Studio IDE | Continue without code | Tools | Android | Android Device Manager | Start den enhed som du har lavet lige før`

<br />


### 2. Kører den afhængig repository
Kører [Efficient Secure Transport](https://github.com/nitram1337/EfficientSecureTransport) som *Multiple-startup* med de følgende microservices i rækkefølgen:
- **EST.API.IdentityMS**
- **EST.API.Gateway**
- **EST.API.StorageMS**
- **EST.API.RouteMS**
- **EST.API.HotspotMS**

<br />


### 3. Kører runAdb.ps1
`EST-Mobile` har en powershell script som vil køre adb kommandoer automatisk for dig. Scriptet  *runAdb.ps1* findes ind i `EST-Mobile` solution ind i mappen *Adb*.
 >**Note:** Du kan evt. bruge [Web Compiler](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebCompiler) extension for hurtigt at køre scriptet ved at højreklikke.
 `EST-Mobile | Adb solution folder | hørjeklik på runAdb.ps1 | Execute file`
  >**Note:** Du skal IKKE lukke powershell fanen, eller så `EST-Mobile` kan ikke nå microservicer.

<br />


### 4. Kører EST-Mobile
`EST-Mobile` må godt køres nu ved at trykke *start*.

<br />


# Mock-ups
>**Note:** Nogle af siderne er ikke helt færdige, men i fremtiden er det sådan, appen skal ses.
<div style="display: flex;">
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_Splash_Screen.png" height="380" />
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_Login_Page.png" height="380" />
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_Flyout_Menu.png" height="380" />
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_Current_Route_Page.png" height="380" />
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_All_Routes_Page.png" height="380" />
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_My_Profile_Page.png" height="380" />
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_All_Routes_Page.png" height="380" />
    <img src="https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/Screens/EST_MobileApp_My_Profile_Page.png" height="380" />
</div>
Mock-ups blev lavet med <a href="https://www.figma.com/">Figma</a>. Et open-source værktøj til at designe logoer, WebUI og MobileUI.


<br />
<br />


# In depth look
En kort [flowchart her](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/EST_Mobileapp_Flowchart.png), som den viser hvordan en virksomhedens lastbilchauffør opdater sin nuværende lokation, imens er igang med en rute.
Flowchart blev lavet med [draw.io](draw.io).
<br />

Her under vises et sekvens diagram for at forstå hvordan kommunikationen imellem `EST-Mobile` og **Api.Gateway** fra [Efficient Secure Transport](https://github.com/nitram1337/EfficientSecureTransport)

![Communication between EST-MobileApp and Api.Gateway ](https://github.com/nitram1337/EfficientSecureTransport-Mobile/blob/master/Images/EST_Mobileapp_Sequence_Diagram_ApiGateway.png)
Sekvens diagramet blev lavet med [sequencediagram.org](https://sequencediagram.org/).
<br />

`EST-Mobile` bruge [MVVM (Model-View-ViewModel-Model) pattern](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm) for afkobling business logic og presentation logic.

<br />


# Licenses
`EST-Mobile` bruger nogle third-party assets:
- [Xamarin.Plugins](https://github.com/jamesmontemagno/Xamarin.Plugins) lavet af James Montemagno.
- [XamarinCommunityToolkit](https://github.com/xamarin/XamarinCommunityToolkit) lavet af Xamarin Dev Team.
- [IdentityModel.OidcClient](https://github.com/IdentityModel/IdentityModel.OidcClient) lavet af Duende Software.
- [Fontawesome ikoner](https://fontawesome.com/v6/search) lavet af Fontawesome Team.
- Nogle tekst font hentet fra [Google Fonts](https://fonts.google.com/).
