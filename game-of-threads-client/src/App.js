import React, { Component } from 'react';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props)
  }

  componentDidMount() {
    (function () {
      var e = document.createElement("script");
      e.async = true;
      e.defer = true;
      e.src = "https://apis.google.com/js/client:platform.js?onload=gPOnLoad";
      var t = document.getElementsByTagName("script")[0];
      t.parentNode.insertBefore(e, t)
    })();
  }

  googleLogin = () => {
    window.gapi.auth.signIn({
      callback: function (authResponse) {
        this.googleSignInCallback(authResponse)
      }.bind(this),
      clientid: "292122738397-rabof0ms7ocsb53k6kt23gg0aoqillur.apps.googleusercontent.com",
      cookiepolicy: "single_host_origin",
      requestvisibleactions: "http://schema.org/AddAction",
      scope: "profile email"
    });
  }

  googleSignInCallback = (e) => {
    if (e["status"]["signed_in"]) {
      window.gapi.client.load("plus", "v1", function () {
        let token = e["access_token"];
        if (token) {
          this.getUserGoogleProfile(token)
        }
      }.bind(this));
    }
  }

  getUserGoogleProfile = (accesstoken) => {
    var e = window.gapi.client.plus.people.get({
      userId: "me"
    });
    e.execute(function (e) {
      if (e.id) {
        console.log(accesstoken)
        console.log(e.email);
      }
    }.bind(this));
  }

  render() {
    return (
      <div onClick={() => this.googleLogin()}>Google</div>
    )
  }
}

export default App;
