﻿import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from '../content.jsx';
import Loading from '../loading.jsx';
import SecureRouteComponent from '../secure_route_component.jsx';

class HistoryDetails extends SecureRouteComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            data: null
        }

        /* there has to be a better way to do this, but I don't
        want to load these files for every page, just the handful
        that need a grid */
        this.loadCss('https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css')
            .then(() => {
                this.loadCss('https://cdn.datatables.net/rowgroup/1.1.1/css/rowGroup.dataTables.min.css');
            });

        this.loadScript('https://code.jquery.com/jquery-3.4.1.min.js')
            .then(() => {
                this.loadScript('https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js')
                    .then(() => {
                        this.loadScript('https://cdn.datatables.net/rowgroup/1.1.1/js/dataTables.rowGroup.min.js')
                            .then(() => {
                                this.getHistoryDetails()
                                    .then(resp => {
                                        this.setState({ isLoading: false, data: resp});
                                    })
                            });
                    });
            });
    }

    initGrid() {
    }

    componentDidUpdate() {
        if (this.state && this.state.data && this.state.data.length > 0) {
            this.initGrid();
        }
    }

    getHistoryDetails() {
        var token = this.auth.getCurrentToken();
        var data = { token: token, sequenceId: this.props.match.params.sequenceId };
        return fetch('/api/User/HistoryDetails', {
                method: "Post",
                headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
                , body: JSON.stringify(data)
            })
            .then(resp => resp.json())
            .then(data => {
                return Promise.resolve(data);
            });
    }

    render() {
        if (this.authenticationComplete()) {

            return (
                <ContentContainer authed={true}>
                    <h2>History Details</h2>

                    <div className="content_row_sm">
                        <Loading showLoading={this.state.isLoading} />
                        <h3>Text Presented to You</h3>

                        <form>
                            <textarea name="textShown"
                                value={
                                    (this.state.data != null) ?
                                    this.state.data.TextShown : ""
                                }
                                readOnly
                            />
                        </form>
                        
                    </div>

                    <div className="content_row_sm">
                        <Loading showLoading={this.state.isLoading} />
                        <h3>Text You Entered</h3>

                        <form>
                            <textarea name="textEntered"
                                value={
                                    (this.state.data != null) ? 
                                        this.state.data.TextEntered : ""
                                }
                                readOnly
                            />
                        </form>
                    </div>

                    <div className="content_row_sm">
                        <Loading showLoading={this.state.isLoading} />
                        <h3>Text You Entered</h3>
                        
                    </div>
                </ContentContainer>
            );
        }
        else {
            return (
                super.render()
            )
        }
    }
}

export default HistoryDetails;