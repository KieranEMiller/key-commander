class Auth {
    constructor(params) {
        this.DEBUG = true;
        this.URL_BASE = "http://localhost:8080/api/auth";
        this.LOCAL_STORAGE_KEY = "key-cdr-jwt";

        //this.Login = this.Login.bind(this);
    }

    getCurrentToken() {
        var token = localStorage.getItem(this.LOCAL_STORAGE_KEY);
        try {
            if (token)
                return JSON.parse(token);
        }
        catch (err) {
            console.log('token parse error: ', err);
        }

        return null;
    }

    clearToken() {
        localStorage.removeItem(this.LOCAL_STORAGE_KEY);
    }

    setToken(token) {
        var tokenstring = JSON.stringify(token);
        localStorage.setItem(this.LOCAL_STORAGE_KEY, tokenstring);
    }

    isAuthenticated() {
        if (this.DEBUG) console.log("auth: isauthenticated: started");
        var token = this.getCurrentToken();
        if (!token) {
            if (this.DEBUG) console.log("auth: isauthenticated: no token found");
            return false;
        }

        this.isValidToken(token)
            .then(ret => {
                if (this.DEBUG) console.log("auth: isauthenticated: server check was ", ret);
                return ret.isvalid;
            });

        //this is wrong, we dont really know if the token is valid yet
        return true;
    }

    logout() {
        if (this.DEBUG) console.log("auth: logging out");
        this.clearToken();
    }

    login(user) {
        return fetch(this.URL_BASE + "/login", {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json'},
            body: JSON.stringify(user)
        })
        .then(resp => resp.json())
        .then(data => {
            if(this.DEBUG) console.log("login return: ", data);
            if (data.isvalid) {
                this.setToken(data);
            } 
            return Promise.resolve(data);
        })
    }

    isTokenExpired(token) {
        try {
            const decoded = decode(token);
            if (decoded.exp < Date.now() / 1000) { 
                return true;
            }
            else
                return false;
        }
        catch (err) {
            return false;
        }
    }

    isValidToken() {
        var token = this.getCurrentToken();
        if (!token || !token.jwt) return false;

        return fetch(this.URL_BASE + "/validate", {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json'
                //'Authorization': `Bearer ${token}`
            }
            ,body: JSON.stringify(token)
        })
        .then(resp => resp.json())
        .then(data => {
            if (!data.isvalid) {
                this.clearToken();
            }
            return Promise.resolve(data);
        })
    }
};

export default Auth;
