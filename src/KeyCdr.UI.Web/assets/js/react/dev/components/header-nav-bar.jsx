import React from 'react';
import ReactDOM from 'react-dom';
import { Link, withRouter } from "react-router-dom";

import Auth from '../auth.jsx'
import { Routes } from '../constants.jsx';

class HeaderNavBar extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            currentRoute: this.props.location.pathname,
            routes: [
                { route: Routes.INDEX, display: "Home", active: false },
                { route: Routes.MY_ACCT, display: "My Account", active: false }
            ]
        };
    };

    clickHandler(route) {
        this.setState(prevState => {
            const list = prevState.routes.map((item, j) => {
                item.active = (item.route === route);
                return item;
            });

            return { list };
        });
    };

    render() {

        let link;
        if (this.props.IsAuthed === true) {
            link = <Link to={Routes.LOGOUT} onClick={this.props.appLogout}>Logout</Link>;
        } else {
            link = <Link to={Routes.LOGIN} onClick={() => this.clickHandler(Routes.LOGIN)}>Login</Link>;
        }

        var validRoutes = [Routes.MY_ACCT, Routes.HISTORY, Routes.HISTORY_VISUAL, Routes.NEW_SEQUENCE];
        let showSubMenu = (
                (validRoutes.indexOf(this.props.location.pathname) >= 0)
                || this.props.location.pathname.indexOf(Routes.HISTORY_DETAILS) >= 0
            ) && (this.props.IsAuthed === true);

        return (
            <React.Fragment>
                <div id="nav_container" className="nav_container">
                    <ul>
                        {this.state.routes.map((route, index) => {
                            var css = (route.active) ? 'current' : '';
                            return (
                                <li key={index} className={css}>
                                    <Link to={route.route} onClick={() => this.clickHandler(route.route)}>{route.display}</Link>
                                </li>
                            )   
                        })}
                        <li>{link}</li>
                    </ul>
                </div>
                {
                    /* submenu visible only for my account */
                    (showSubMenu === true) &&
                    <div id="sub_nav_container" className="nav_container_sm">
                        <ul>
                            <li>
                                <Link to={Routes.NEW_SEQUENCE} onClick={() => this.clickHandler(Routes.NEW_SEQUENCE)}>New Sequence</Link>
                            </li>
                            <li> 
                                <Link to={Routes.HISTORY} onClick={() => this.clickHandler(Routes.HISTORY)}>History</Link>
                            </li>
                            <li> 
                                <Link to={Routes.HISTORY_VISUAL} onClick={() => this.clickHandler(Routes.HISTORY_VISUAL)}>Visual History</Link>
                            </li>
                        </ul>
                    </div>
                }
                
            </React.Fragment>
        );
    }
}

export default withRouter(HeaderNavBar);