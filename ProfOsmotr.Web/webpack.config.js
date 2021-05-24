const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

const generalConfig = {
    entry: path.resolve(__dirname, 'wwwroot/js/src/main.js'),
    output: {
        path: path.resolve(__dirname, 'wwwroot/js/dist'),
        filename: '[name].js'
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery"
        }),
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin(
            {
                filename: "../../css/dist/[name].css"
            }),
    ],
    optimization: {
        splitChunks: {
            cacheGroups: {
                vendor: {
                    test: /node_modules/,
                    chunks: 'initial',
                    name: 'vendor',
                    enforce: true
                },
            }
        }
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, 'css-loader']
            }
        ]
    }
}

const reactConfig = {
    entry: {
        index: path.resolve(__dirname, 'OperatorClientApp/index.js')
    },
    output: {
        path: path.resolve(__dirname, 'wwwroot/js/dist'),
        filename: "operator-workplace.js"
    },
    module: {
        rules: [
            {
                use: {
                    loader: "babel-loader"
                },
                test: /\.(js|jsx)$/,
                exclude: /node_modules/
            },
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, 'css-loader']
            }
        ]
    },
    devtool: 'inline-source-map'
}


module.exports = [generalConfig, reactConfig];