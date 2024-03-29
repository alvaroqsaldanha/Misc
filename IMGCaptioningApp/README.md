# Image Captioning App

App that provides a clean and fast interface to get captions for images, both of device's camera and gallery. Additionally, the user can navigate and manage previous captioned images, through a dedicted caption history screen.

## Technologies Used:

- ReactJS: Basis for app development, with TypeScript.
- MaterialUI: for styling and animations.
- Capacitor: for building the app.
- @capacitor-community/sqlite: for storage of image/caption history.
- @capacitor/camera: for taking pictures using device's camera or selecting images from gallery.
- HuggingFace serverless API: to send a selected image as payload to a pre-trained image captioning model API, and receive a caption as a reply.
- Monaca (optional): as an environment setup alternative.

## Running the app

For a more detailed view of app setup and running the app, see the below linked blogpost.

### Running on browser:

- `npm install yarn -g`
- `yarn install`
- `yarn dev`

or, if using Monaca:

- `npm i -g monaca`
- `monaca login`
- `monaca preview`

### Running on mobile:

If using yarn:

- `npm install yarn -g`
- `yarn add @capacitor/core @capacitor/ios @capacitor/android`
- `yarn cap add ios`
- `yarn cap add android`
- `yarn cap sync`
- `yarn cap open android`

or, if using Monaca, use Monaca cloud.

## Blogpost

Read a blogpost about this application [here](https://medium.com/the-web-tub/making-a-simple-image-captioning-app-w-monaca-capacitor-reactjs-and-hugging-face-4eb9f9c0fbfd).

## App structure and additional information

The app consists of three simple main screens: menu, caption history, and settings. The menu allows for the user to caption images either by taking a picture with the camera, or by selecting a previously existing image from the device's gallery. The caption history is for navigating and managing the device's previous captions, providing the option to reset previously saved data. The settings allows the user to choose the Hugging Face model to use, as well as toggle dark mode.
