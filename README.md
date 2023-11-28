# NewsAndWeather
## Overview
NewsAndWeather is a mobile application built with C# using the .NET MAUI (Multi-platform App UI) framework. Currently, it is designed to run on Android devices. The app provides users with the latest news sourced from a custom API developed by the author (link to the API repository provided below) and offers a 5-day weather forecast using the AccuWeather API.


<img src="https://github.com/Rafalekvx/NewsAndWeather/assets/111763595/3aa3abfb-893b-481b-8707-5728f5242a10" width="210">
<img src="https://github.com/Rafalekvx/NewsAndWeather/assets/111763595/6c83eb74-4bee-43a4-9673-ebd8b5dd6ad6" width="210">
<img src="https://github.com/Rafalekvx/NewsAndWeather/assets/111763595/c87f2fa8-898e-4dd3-b197-f26fdfa571cf" width="210">
<img src="https://github.com/Rafalekvx/NewsAndWeather/assets/111763595/9d61fd35-29be-4645-9fc1-5ba7e8a6581b" width="210">




## Features
Latest News: Stay informed with the latest news headlines and articles.
5-Day Weather Forecast: Get accurate and up-to-date weather forecasts for the next 5 days.


## Installation

### Download the Latest Release

For a stable version of the app, you can download the latest release from the [releases page](https://github.com/Rafalekvx/NewsAndWeather/releases/latest).

1. Visit the [releases page](https://github.com/Rafalekvx/NewsAndWeather/releases/latest).

2. Download the latest version of the app by clicking on the asset link for your platform (e.g., `com.rafalek.newsandweather.apk`).

3. Install the downloaded APK on your Android device.

   **Note:** Ensure that your device allows installations from unknown sources. You can adjust this setting in your device's security settings.

4. Open the installed app and enjoy the latest news and weather information!

### Clone and Customize:

 **Clone the repository:**
   ```bash
   git clone https://github.com/Rafalekvx/NewsAndWeather
  ```

1. **Open the project in Visual Studio or your preferred IDE.**

2. **Build and run the application on an Android emulator or physical device.**

   Ensure that your development environment is set up correctly for .NET MAUI development. You may refer to the [official documentation](https://docs.microsoft.com/en-us/dotnet/maui/get-started/installation) for installation and setup instructions.

3. **Create a configuration file (e.g., `Resources/Raw/SyncfusionLicense.json`) with the following content:**

    ```json
    ﻿{
      "License": "YOUR SYNCFUSION LICENCE KEY"
    }
    ```

    Replace `YOUR SYNCFUSION LICENCE KEY` with the key of your syncfusion license.

4. **Ensure the necessary NuGet packages are installed.**

5. **Run the application.**

    Once the application is built successfully, run it on your Android emulator or physical device. Ensure that the app launches without any errors.

## API Repositories

- [Custom News API](https://github.com/Rafalekvx/NewsAndWeatherAPI)

## Configuration

Make sure to update the `Resources/Raw/SyncfusionLicense.json` file with your specific configurations for the syncfusion license.

## Technologies Used

- .NET MAUI
- C#
- AccuWeather API
- Custom News API (developed by the author)

## Contributing

Contributions are welcome! Feel free to open issues and submit pull requests.

## Acknowledgments

- Special thanks to AccuWeather for providing the weather data.

---

Adapt the instructions based on any specific requirements for building and running .NET MAUI applications on Android.
