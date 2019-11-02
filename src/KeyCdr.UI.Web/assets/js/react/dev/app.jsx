import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'

import Routing from     './routing.jsx';
import Header from './header.jsx';

import '../../../../assets/css/master.scss';

class App extends React.Component {
    render() {
        return (
            <Router>
                <Routing />
            </Router>
        ); 
    }
};

ReactDOM.render(
    <App/>,
    document.getElementById('app')
);
