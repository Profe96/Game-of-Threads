import React, { Component } from 'react';
import './App.css';

class App extends Component {
  constructor(props) {
    super(props);

    this.onSignIn = this.onSignIn.bind(this);
  }

  onSignIn = (googleUser) => {
    var id_token = googleUser.getAuthResponse().id_token;
    console.log(id_token);
  }

  render() {
    return (
      <div className="g-signin2"></div>
    );
  }
}

export default App;
