import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { BrowserRouter as Router } from 'react-router-dom';
import { CookiesProvider } from 'react-cookie';

ReactDOM.render(
    <CookiesProvider>
        <Router>
            <App />
        </Router>
    </CookiesProvider>, document.getElementById('root')
);
