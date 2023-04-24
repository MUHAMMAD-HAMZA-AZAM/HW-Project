importScripts('https://www.gstatic.com/firebasejs/7.6.0/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/7.6.0/firebase-messaging.js');
firebase.initializeApp({
  apiKey: "AIzaSyCul4qWWQ6Xpp3RNIJNJQzBLfOnnls4j6k",
  authDomain: "hoomwork-location-key.firebaseapp.com",
  databaseURL: "https://hoomwork-location-key.firebaseio.com",
  projectId: "hoomwork-location-key",
  storageBucket: "hoomwork-location-key.appspot.com",
  messagingSenderId: "299568931003",
  appId: "1:299568931003:web:7763ed6bb067ed1a95c99e",
  measurementId: "G-9XNTLMBZVC"
});
const messaging = firebase.messaging();
messaging.setBackgroundMessageHandler(function (payload) {
  let notify_data = payload['data'];
  let title = notify_data['title'];
  let options = {
    body: notify_data['body'],
    icon: '../../ClientApp/src/assets/img/login-logo.png',
    badge: '../../ClientApp/src/assets/img/login-logo.png',
    image: '../../ClientApp/src/assets/img/login-logo.png',
  }
  return self.registration.showNotification(title, options);
});
