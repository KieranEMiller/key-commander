﻿
export const Urls = {
    API_AUTH_VALIDATE: "http://keycommander.azurewebsites.net/api/auth/validate",
    API_AUTH_LOGIN: "http://keycommander.azurewebsites.net/api/auth/login",
    API_AUTH_USERNAME_AVAILABLE: "http://keycommander.azurewebsites.net/api/auth/IsUsernameInUse",
    API_AUTH_REGISTER: "http://keycommander.azurewebsites.net/api/auth/register",

    API_HISTORY: "/api/User/History",
    API_HISTORY_DETAILS: "/api/User/HistoryDetails",
    API_HISTORY_VISUAL: "/api/User/HistoryAnalytics",

    API_NEW_SESSION: "/api/Session/GetNewSession",
    API_NEW_SEQUENCE: "/api/Sequence/GetNewSequence",
    API_END_SEQUENCE: "/api/Sequence/EndSequence",
    API_RUN_ANALYSIS_FOR_SEQ: "/api/ErrorAnalysis/RunForSequence",

    External: {
        JS: {
            JQUERY: "https://code.jquery.com/jquery-3.4.1.min.js",
            DATATABLES_CORE: "https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js",
            DATATABLES_ROWGROUP: "https://cdn.datatables.net/rowgroup/1.1.1/js/dataTables.rowGroup.min.js"
        },

        CSS: {
            DATATABLES_CORE: "https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css",
            DATATABLES_ALT: "https://cdn.datatables.net/rowgroup/1.1.1/css/rowGroup.dataTables.min.css"
        }
    }
}

export const Routes = {
    DEFAULT: "/",
    INDEX: "/",

    LOGIN: "/Login",
    LOGOUT: "/Logout",
    REGISTER: "/Register",
    NEW_SEQUENCE: "/secure/NewSequence",

    MY_ACCT: "/secure/MyAccount",
    HISTORY: "/secure/History",
    HISTORY_DETAILS: "/secure/HistoryDetails",
    HISTORY_DETAILS_WITHID: "/secure/HistoryDetails/:sequenceId",
    HISTORY_VISUAL: "/secure/History/Visual"
}

export const LocalStorage = {
    JWT_KEY_NAME: "key-cdr-jwt",
    SESSION_KEY_NAME: "key-cdr-session"
}

export const Runtime = {
    IS_DEBUG: true,
    DEFAULT_NEW_SEQUENCE_INPUT_FIELD_HEIGHT: 75
}

export const UserMsgs = {
    AUTH_FAILURE: "unable to login with the username and password provided",
    NEW_SEQUENCE_INSTRUCTIONS: "PRESS SHIFT+ENTER OR PRESS START TO BEGIN",

    Registration: {
        PASSWORD_MATCH: "both your passwords must match",
        USERNAME_IN_USE: "the username {0} is already in use",
        MUST_BE_NON_EMPTY: "must be non empty"
    }
}

export const HighlightType = {
    0: "IncorrectChar",
    1: "ExtraChars", 
    2: "ShortChars", 
    3: "Normal",
    4: "Unevaluated"
}

export const HttpErrorHandler = function (response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}

export const RealTimeAnalysisSettings = {
    LENGTH_BUFFER: 5,
    TICK_INTERVAL_IN_MS: 5000,
    UNCHANGED_TEXT_TICK_THRESHOLD: 5
}
