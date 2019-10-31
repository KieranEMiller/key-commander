﻿import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'

import Header from      './header.jsx';
import Index from       './route_index.jsx';
import About from       './route_about.jsx';
import Login from       './route_login.jsx';

class Routing extends React.Component {
    render() {
        return (
            <div>
                <Switch>
                    <Route exact path="/" component={Index} />
                    <Route exact path="/About" component={About} />
                    <Route exact path="/Login" component={Login} />
                    <Route component={Index} />
                </Switch>
            </div>
        );
    }
}

export default Routing;