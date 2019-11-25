import Auth from                    './auth.jsx';
import { Urls, LocalStorage } from  './constants.jsx';

class SessionManager {
    constructor(props) {
        this.state = {
            UserId: null,
            SessionId: null,
            SequenceId: null,
            StartTime: null
        }
    }

    sequenceStarting() {
        this.state.StartTime = new Date();
    }

    startNewSession = () => {
        var auth = new Auth();
        var user = auth.getCurrentToken();

        var that = this;
        return fetch(Urls.API_NEW_SESSION, {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' },
            body: JSON.stringify(user)
        })
        .then(resp => resp.json())
        .then(data => {
            that.state.SessionId = data.SessionId;
            that.state.UserId = data.UserId;
            that.persistSessionSeqData(this.state);
            return Promise.resolve(data);
        });
    }

    getCurrentSession() {
        var session = localStorage.getItem(LocalStorage.SESSION_KEY_NAME);
        try {
            if (session)
                return JSON.parse(session);
        }
        catch (err) {
            console.log('session mgr: token parse error: ', err);
        }

        return null;
    }

    clearSession() {
        localStorage.removeItem(LocalStorage.SESSION_KEY_NAME);
    }

    persistSessionSeqData(data) {
        var sessionData = JSON.stringify(data);
        localStorage.setItem(LocalStorage.SESSION_KEY_NAME, sessionData);
    }

    getNewSequence = () => {
        var that = this;
        return new Promise(function (resolve) {
            /* if the user does not have an existing session, make a new one */
            var currentSession = that.getCurrentSession();
            if (!currentSession || !currentSession.SessionId) {
                that.startNewSession()
                    .then(newSession => {
                        that.getNewSequenceWithSession(newSession)
                            .then(data => { resolve(data); });
                    });
            }
            else {
                that.getNewSequenceWithSession(currentSession)
                    .then(data => { resolve(data); });
            }
        });
    }

    getNewSequenceWithSession = (session) => {
        return fetch(Urls.API_NEW_SEQUENCE, {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' },
            body: JSON.stringify(session)
        })
            .then(resp => resp.json())
            .then(data => {
                this.state.SequenceId = data.SequenceId;
                
                this.persistSessionSeqData(this.state);
                return Promise.resolve(data);
            });
    }

    stopCurrentSequence = (usersText) => {
        var elapsed = new Date() - this.state.StartTime;

        var currentSession = this.getCurrentSession();
        if (!currentSession || !currentSession.SessionId) return Promise.reject();

        currentSession.ElapsedInMilliseconds = elapsed;
        currentSession.TextEntered = usersText;

        return fetch(Urls.API_END_SEQUENCE, {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json' },
            body: JSON.stringify(currentSession)
        })
            .then(resp => resp.json())
            .then(data => {
                return Promise.resolve(currentSession);
            });
    }
}

export default SessionManager;