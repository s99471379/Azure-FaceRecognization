# Azure-FaceRecognization

This project demonstrates the use of Azure's FaceAPI for facial recognition. It can identify a person's emotions, age, and gender. Additionally, it can be integrated into access control systems using 3D facial recognition and provides insights into leveraging cloud-based AI computation.

---

## Features

- **Facial Attribute Detection**: Identifies age, gender, and emotions.
- **Cloud-Based Processing**: Utilizes Azure FaceAPI for efficient AI computation.
- **Easy Integration**: Can be extended to access control systems with 3D facial recognition.
- **Image Processing**: Supports image input via local files or URLs.

---

## Technology Stack

- **Platform**: Azure Cloud (FaceAPI)
- **Language**: C#
- **API**: Azure Cognitive Services FaceAPI

---

## Installation and Setup

1. Create a FaceAPI resource in Azure:
   - Go to the Azure portal and create a new FaceAPI resource.
   - Retrieve the API Key and Endpoint from the Azure portal.

2. Clone the repository:

   ```bash
   git clone https://github.com/<your-username>/FaceAPI-Facial-Recognition.git
   cd FaceAPI-Facial-Recognition
   ```

3. Configure the API Key and Endpoint in the code:
   - Open the C# project and update the following values:
     ```csharp
     const string subscriptionKey = "<Your-API-Key>";
     const string faceEndpoint = "https://<your-region>.api.cognitive.microsoft.com/face/v1.0/detect";
     ```

4. Build and run the application in your C# development environment.

---

## Usage

1. Import an image:
   - Use a local file or provide a URL to the image.
2. Process the image:
   - The application will send a request to Azure FaceAPI using `MakeRequest`.
3. View results:
   - The response includes facial attributes such as age, gender, and emotions.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

---

## Acknowledgments

This project leverages Azure Cognitive Services FaceAPI to demonstrate the power of cloud-based AI for facial recognition.
