﻿import React from 'react';
import ReactDOM from 'react-dom';
import Auth from './auth.jsx';
import { Route, Redirect} from 'react-router-dom'

/* no longer used; routes requiring authentication now
extend SecureRouteComponent leaving this here for reference */
const SecureRoute = ({ component: Component, ...props }) => {
    var auth = new Auth();
    return (
        <Route
            {...props}
            render={innerProps =>
                auth.isAuthenticated() ?
                    <Component {...props} />
                    : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />   
                }
        />
    );

};

export default SecureRoute;