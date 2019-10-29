import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'

import Header from      './header.jsx';
import Index from       './route_index.jsx';
import About from       './route_about.jsx';

const routing = (
    <Router>
        <React.Fragment>
            <Header />
            <Switch>
                <Route exact path="/" component={Index} />
                <Route exact path="/about" component={About} />
                <Route component={Index} />
            </Switch>
        </React.Fragment>
    </Router>
)

ReactDOM.render(
    routing,
    document.getElementById('app')
);
