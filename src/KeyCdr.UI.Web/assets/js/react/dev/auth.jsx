class Auth {
    constructor(params) {
        this.DEBUG = true;
        this.URL_BASE = "http://localhost:8080/api/auth";
        this.LOCAL_STORAGE_KEY = "key-cdr-jwt";

        //this.Login = this.Login.bind(this);
    }

    GetCurrentToken() {
        return localStorage.getItem(this.LOCAL_STORAGE_KEY);
    }

    ClearToken() {
        localStorage.removeItem(this.LOCAL_STORAGE_KEY);
    }

    SetToken(token) {
        localStorage.setItem(this.LOCAL_STORAGE_KEY, token);
    }

    IsAuthenticated() {
        var token = this.GetCurrentToken();
        return token && this.IsValidToken(token);
    }

    Logout() {
        this.ClearToken();
    }

    Login(user) {
        return fetch(this.URL_BASE + "/login", {
            method: "POST",
            headers: { 'Content-Type': 'application/json', Accept: 'application/json'},
            body: JSON.stringify(user)
        })
        .then(resp => resp.json())
        .then(data => {
            if(this.DEBUG) console.log("login return: ", data);
            if (data.isvalid) {
                this.SetToken(data);
            } 
            return Promise.resolve(data);
        })
    }

    IsTokenExpired(token) {
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

    IsValidToken() {
        var token = this.GetCurrentToken();
        if (!token) return false;

        return fetch(this.URL_BASE + "/validate", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                Accept: 'application/json'
                //'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(token)
        })
        .then(resp => resp.json())
        .then(data => {
            if (!data.isvalid) {
                this.ClearToken();
            }
            return Promise.resolve(data);
        })
    }
};

export default Auth;
