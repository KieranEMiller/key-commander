"use strict";

var path = require("path");
var WebpackNotifierPlugin = require("webpack-notifier");
var BrowserSyncPlugin = require("browser-sync-webpack-plugin");
var MiniCssExtractPlugin = require("mini-css-extract-plugin");

const Paths = {
    React: {
        Entry: path.resolve(__dirname, './assets/js/react/dev/app.jsx'),
        Output: path.resolve(__dirname, './assets/js/react/dist')
    },
    Css: path.resolve(__dirname, './assets/css')
};

module.exports = {
    entry: Paths.React.Entry, 
    output: {
        path: Paths.React.Output,
        filename: "bundle.js"
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            },
            {
                test: /\.(sa|sc|c)ss$/,
                loader: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    {
                        loader: 'sass-loader',
                    }
                ]
            }
        ]
    },

    /*for debugging: preserve stack trace */
    devtool: "inline-source-map",

    plugins: [
        new WebpackNotifierPlugin(),
        new MiniCssExtractPlugin({
            filename: '../../../../assets/css/styles.css'
        })
        /*, new BrowserSyncPlugin()*/
    ]
};