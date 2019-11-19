import React from 'react';
import ReactDOM from 'react-dom';
import { Redirect} from 'react-router-dom'

import ContentContainer from    '../content.jsx';
import Loading from             '../loading.jsx';
import SecureComponent from     './secure_component.jsx';
import { Urls } from            '../constants.jsx';

class History extends SecureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            data: null
        }

        this.loadDependencies();
    }

    loadDependencies() {
        /* there has to be a better way to do this, but I don't
        want to load these files for every page, just the handful
        that need a grid */
        this.loadCss(Urls.External.CSS.DATATABLES_CORE)
            .then(() => {
                this.loadCss(Urls.External.CSS.DATATABLES_ALT);
            });

        this.loadScript(Urls.External.JS.JQUERY)
            .then(() => {
                this.loadScript(Urls.External.JS.DATATABLES_CORE)
                    .then(() => {
                        this.loadScript(Urls.External.JS.DATATABLES_ROWGROUP)
                            .then(() => {
                                this.getUserHistory()
                                    .then(resp => {
                                        this.setState({ isLoading: false, data: resp });
                                    });
                            });
                    });
            });
    }

    initGrid() {

        $('#grid').DataTable({
            data: this.state.data,

            /* to enable grouping...*/
            /* rowGroup: { dataSrc: 'SessionId' },*/

            scrollY: 250,
            order: [[0, "desc"]],
            columns: [
                { data: "CreateDate" },
                { data: "SourceType" },
                { data: "SourceKey" },
                {
                    "data": "KeySequenceId",
                    "render": function (data, type, row, meta) {
                        if (type === 'display') {
                            data = '<a class="historyDetailsLink" href="/secure/HistoryDetails/' + data + '">details</a>';
                        }

                        return data;
                    }
                } 
            ]
        });

        /* bind to the click events of the details column on the grid
        this allows you to push history and keep the react router type of
        navigation and avoids a full page refresh */
        var that = this;
        $("a.historyDetailsLink").click(function (e) {
            e.preventDefault();
            that.props.history.push($(this).attr('href'));
        });
    }

    getUserHistory() {
        var token = this.auth.getCurrentToken();
        return fetch(Urls.API_HISTORY, {
                method: "Post",
                headers: { 'Content-Type': 'application/json', Accept: 'application/json' }
                , body: JSON.stringify(token)
            })
            .then(resp => resp.json())
            .then(data => {
                return Promise.resolve(data);
            });
    }

    componentDidUpdate() {
        if (this.state && this.state.data && this.state.data.length > 0) {
            this.initGrid();
        }
    }

    render() {
        if (this.authenticationComplete()) {
            return (
                <ContentContainer authed={true}>
                    <h2>Session History</h2>

                    <div className="content_row">
                        <Loading showLoading={this.state.isLoading} />
                        
                        <table id="grid" className="display cell-border stripe hover">
                            <thead>
                                <tr>
                                    <th>CreateDate</th>
                                    <th>SourceType</th>
                                    <th>SourceKey</th>
                                    <th>Details</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
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

export default History;