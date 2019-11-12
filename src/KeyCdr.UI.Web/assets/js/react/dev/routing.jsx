import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'

import SecureRoute from     './secure_route.jsx';
import Header from          './header.jsx';

import Index from           './paths/index.jsx';
import About from           './paths/about.jsx';
import Login from           './paths/login.jsx';
import MyAccount from       './paths/myaccount.jsx';
import History from         './paths/history.jsx';
import HistoryDetails from  './paths/history_details.jsx';

class Routing extends React.Component {
    render() {
        return (
            <div>
                <Header /> 
                <Switch>
                    <Route exact path="/" component={Index} />
                    <Route exact path="/Logout" component={Index} />
                    <Route exact path="/About" component={About} />
                    <Route exact path="/Login" component={Login} />

                    <Route exact path="/secure/MyAccount" component={MyAccount} />

                    <Route exact path="/secure/History" component={History} />
                    <Route path="/secure/HistoryDetails" component={HistoryDetails} />

                    <Route component={Index} />
                </Switch>
            </div>
        );
    }
}

export default Routing;