import React, { Component } from 'react';
import './App.css';

class Login extends Component {
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
        let token = e["id_token"];
        if (token) {
            this.setState({ token: token });
            this.callApi().then(res => {
                this.props.history.push(`/home/${res.name}`);
            });
        }
    }

    callApi = async () => {
        const response = await fetch(`/google/auth?idToken=${this.state.token}`);
        const body = await response.json();
        if (response.status !== 200) throw Error(body);
        return body;
    };

    render() {
        return (
            <div className="g-signin2 googleButtonSignIn" onClick={() => this.googleLogin()} />
        )
    }
}

export default Login;