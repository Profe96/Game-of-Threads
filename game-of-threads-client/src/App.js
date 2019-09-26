import React, { Component } from 'react';
import { Route, Switch } from 'react-router-dom';
import { instanceOf } from 'prop-types';
import { withCookies, Cookies } from 'react-cookie';
import './App.css';

import Login from './Login';
import Home from './Home';

class App extends Component {
  static propTypes = {
    cookies: instanceOf(Cookies).isRequired
  };

  render() {
    return (
      <Switch>
        <Route exact path="/" render={(props) => <Login {...props} cookies={this.props.cookies} />} />
        <Route exact path="/home/:id" render={(props) => <Home {...props} cookies={this.props.cookies} />} />
      </Switch>
    );
  }
}


export default withCookies(App);
