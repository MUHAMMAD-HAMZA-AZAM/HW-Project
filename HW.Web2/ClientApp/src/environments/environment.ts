// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  webBaseUrl: 'http://localhost:52593/api/',
  firebase: {
    apiKey: "AIzaSyCul4qWWQ6Xpp3RNIJNJQzBLfOnnls4j6k",
    authDomain: "hoomwork-location-key.firebaseapp.com",
    databaseURL: "https://hoomwork-location-key.firebaseio.com",
    projectId: "hoomwork-location-key",
    storageBucket: "hoomwork-location-key.appspot.com",
    messagingSenderId: "299568931003",
    appId: "1:299568931003:web:7763ed6bb067ed1a95c99e",
    measurementId: "G-9XNTLMBZVC"
  }
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
