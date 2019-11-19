import React from 'react';
import ReactDOM from 'react-dom';
import { Route, Link, BrowserRouter as Router, Switch } from 'react-router-dom'

import Header from          './header.jsx';
import Index from           './paths/index.jsx';
import About from           './paths/about.jsx';
import Login from           './paths/login.jsx';
import MyAccount from       './paths/myaccount.jsx';
import History from         './paths/history.jsx';
import HistoryDetails from  './paths/history_details.jsx';
import NewSequence from     './paths/new_sequence.jsx';

class Routing extends React.Component {
    render() {
        return (
            <div>
                <Switch>
                    <Route exact path="/" component={Index} />
                    <Route exact path="/Logout" component={Index} />
                    <Route exact path="/About" component={About} />
                    <Route exact path="/NewSequence" component={NewSequence} />

                    <Route
                        exact path="/Login"
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
                        exact path="/secure/MyAccount"
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
                        exact path="/secure/History"
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
                        exact path="/secure/HistoryDetails/:sequenceId"
                        render={
                            (props) =>
                                <HistoryDetails {...props}
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