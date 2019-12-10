import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'

import Header from          './header.jsx';
import Index from           './paths/index.jsx';
import About from           './paths/about.jsx';
import Login from           './paths/login.jsx';
import Register from        './paths/register.jsx';
import MyAccount from       './paths/myaccount.jsx';
import History from         './paths/history.jsx';
import HistoryDetails from  './paths/history_details.jsx';
import HistoryVisual from   './paths/history_visual.jsx';
import NewSequence from     './paths/new_sequence.jsx';
import { Routes } from      './constants.jsx';

class Routing extends React.Component {
    render() {
        return (
            <div>
                <Switch>
                    <Route exact path={Routes.DEFAULT} component={Index} />
                    <Route exact path={Routes.INDEX} component={Index} />

                    <Route
                        exact path={Routes.NEW_SEQUENCE}
                        render={
                            (props) =>
                                <NewSequence {...props}
                                    IsAuthed={this.props.IsAuthed}
                                    appLogin={this.props.appLogin}
                                    appLogout={this.props.appLogout}
                                />
                        }
                    />

                    <Route
                        exact path={Routes.LOGIN}
                        render={
                            (props) =>
                                <Login {...props}
                                    IsAuthed={this.props.IsAuthed}
                                    appLogin={this.props.appLogin}
                                    appLogout={this.props.appLogout}
                                />
                        } 
                    />
                    
                    <Route
                        exact path={Routes.REGISTER}
                        render={
                            (props) =>
                                <Register {...props}
                                    IsAuthed={this.props.IsAuthed}
                                    appLogin={this.props.appLogin}
                                    appLogout={this.props.appLogout}
                                />
                        } 
                    />

                    <Route
                        exact path={Routes.MY_ACCT}
                        render={
                            (props) =>
                                <MyAccount {...props}
                                    IsAuthed={this.props.IsAuthed}
                                    appLogin={this.props.appLogin}
                                    appLogout={this.props.appLogout}
                                />
                        }
                    />

                    <Route
                        exact path={Routes.HISTORY}
                        render={
                            (props) =>
                                <History {...props}
                                    IsAuthed={this.props.IsAuthed}
                                    appLogin={this.props.appLogin}
                                    appLogout={this.props.appLogout}
                                />
                        }
                    />

                    <Route
                        exact path={Routes.HISTORY_DETAILS_WITHID}
                        render={
                            (props) =>
                                <HistoryDetails {...props}
                                    IsAuthed={this.props.IsAuthed}
                                    appLogin={this.props.appLogin}
                                    appLogout={this.props.appLogout}
                                />
                        }
                    />

                    <Route
                        exact path={Routes.HISTORY_VISUAL}
                        render={
                            (props) =>
                                <HistoryVisual {...props}
                                    IsAuthed={this.props.IsAuthed}
                                    appLogin={this.props.appLogin}
                                    appLogout={this.props.appLogout}
                                />
                        }
                    />

                    <Route component={Index} />
                </Switch>
            </div>
        );
    }
}

export default Routing;