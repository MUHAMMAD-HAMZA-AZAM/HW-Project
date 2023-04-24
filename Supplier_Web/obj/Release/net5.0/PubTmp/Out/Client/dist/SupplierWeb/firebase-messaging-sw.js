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
  //apiKey: "AIzaSyDk_mqLrzZhsJDC8FKL082KHXCUwiCJcTo",
  //authDomain: "fir-f8048.firebaseapp.com",
  //projectId: "fir-f8048",
  //storageBucket: "fir-f8048.appspot.com",
  //messagingSenderId: "854595730322",
  //appId: "1:854595730322:web:f86c358abbd89b0f571f4b",
  //measurementId: "${config.measurementId}"

});
const messaging = firebase.messaging();
messaging.setBackgroundMessageHandler(function (payload) {
  console.log('[firebase-messaging-sw.js] Received background message ', payload);
  let notify_data = payload['data'];
  let title = notify_data['title'];
  let data = payload['data'];
  let options = {
    body: notify_data['body'],
    icon: '../../assets/images/logo.png',
    badge: '../../assets/images/logo.png',
    image: '../../assets/images/logo.png'
  }
  return self.registration.showNotification(title, options);
});
