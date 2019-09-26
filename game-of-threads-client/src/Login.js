import React, { Component } from 'react';
import { GoogleLogin } from 'react-google-login';
import './App.css';

class Login extends Component {
    constructor(props) {
        super(props);

        this.googleSignInCallback = this.googleSignInCallback.bind(this);
    }

    googleSignInCallback = (response) => {
        let token = response.Zi.id_token;
        if (token) {
            this.setState({ token: token });
            this.callApi().then(res => {
                this.props.history.push(`/home/${res.email}`);
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
            <GoogleLogin className="g-signin2"
                clientId="292122738397-rabof0ms7ocsb53k6kt23gg0aoqillur.apps.googleusercontent.com"
                buttonText="Login"
                onSuccess={this.googleSignInCallback}
                onFailure={this.googleSignInCallback}
            />
        )
    }
}

export default Login;