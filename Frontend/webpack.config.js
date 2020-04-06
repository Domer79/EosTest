var path = require('path');
var webpack = require('webpack');
const bundleOutputDir = './wwwroot/dist';
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const MomentLocalesPlugin = require('moment-locales-webpack-plugin');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const TerserPlugin = require('terser-webpack-plugin');

var baseUrl;
var environment;
var nodeEnv = process.env.NODE_ENV.trim();
var isProd = (process.env.NODE_ENV === 'production') || process.argv.indexOf('-p') !== -1;

var setupApi = () => {
  environment = nodeEnv;
  console.log(`process.env.NODE_ENV="${nodeEnv}"`);
  if (nodeEnv === "production") {
    baseUrl = '"https://localhost:5001"';
    return;
  }

  if (nodeEnv === "test") {
    baseUrl = '"https://localhost:5001"';
    return;
  }

  if (nodeEnv === "staging") {
    baseUrl = '"https://localhost:5001"';
    return;
  }

  if (nodeEnv === "development") {
    baseUrl = '"https://localhost:5001"';
    return;
  }

  baseUrl = '"https://localhost:5001"';
};

setupApi();

module.exports = {
  context: __dirname,
  entry: {
    'main': './src/js/app.ts',
    'theme': './src/css/app.styl'
  },
  mode: isProd ? 'production' : 'development',
  module: {
    rules: [
      {
        test: /\.vue$/,
        loader: 'vue-loader'
      },
      {
        test: /\.s(c|a)ss$/,
        use: [
          'vue-style-loader',
          'css-loader',
          {
            loader: 'sass-loader',
            options: {
              implementation: require('sass')
            }
          }
        ]
      },
      {
        test: /\.ts$/,
        exclude: /node_modules/,
        use: [
          {
            loader: 'babel-loader',
          },
          'ts-loader',
        ],
      },
      {
        test: /\.html$/,
        exclude: /index.html/,
        use: [{
          loader: 'html-loader?interpolate=require'
        }]
      },
      {
        test: /\.styl/,
        use: [
          MiniCssExtractPlugin.loader,
          'css-loader?url=false',
          {
            loader: 'postcss-loader',
            options: {
              sourceMap: true,
              plugins: [
                require('autoprefixer')
              ]
            }
          },
          'stylus-loader'
        ]
      },
      {
        test: /\.js$/,
        loader: 'babel-loader',
        exclude: /node_modules/
      },
      {
        test: /\.(png|jpg|gif|svg|ttf|eot|woff|woff2)$/,
        loader: 'file-loader',
        options: {
          name: '[name].[ext]?[hash]'
        }
      }
    ]
  },
  resolve: {
    alias: {
      'src': path.join(__dirname, '/src'),
      'vue$': 'vue/dist/vue.esm.js'
    },
    extensions: ['.ts', '.js', '.vue', '.json']
  },
  devServer: {
    historyApiFallback: true,
    noInfo: false,
    overlay: true,
    port: 3100,
    contentBase: path.resolve(__dirname, 'wwwroot'),
    clientLogLevel: 'silent'
  },
  performance: {
    hints: false
  },
  output: {
    path: path.join(__dirname, bundleOutputDir),
    filename: '[name].js',
    publicPath: 'dist/'
  },
  devtool: '#eval-source-map',
  plugins: [
    new TerserPlugin({
      parallel: true,
      terserOptions: {
        ecma: 6,
      },
    }),
    new VueLoaderPlugin(),
    new webpack.DefinePlugin({
      _baseUrl_: baseUrl,
      _environment_: `"${environment}"`
    }),
    new MomentLocalesPlugin({
      localesToKeep: ['ru']
    }),
    new MiniCssExtractPlugin({
      filename: '[name].css',
      chunkFilename: '[id].css',
      ignoreOrder: false
    })
  ]
}

if (nodeEnv === 'production' || nodeEnv === 'test' || nodeEnv === 'staging') {
  console.log(process.env.NODE_ENV);
  module.exports.devtool = '#source-map';
  module.exports = {
    ...module.exports,
    // optimization: {
    //   minimizer: [
    //     new UglifyJsPlugin({
    //       sourceMap: true,
    //     })
    //   ]
    // }
  };
  module.exports.plugins = (module.exports.plugins || []).concat([
    new webpack.DefinePlugin({
      'process.env': {
        NODE_ENV: '"production"'
      }
    }),
    // new webpack.LoaderOptionsPlugin({
    //   minimize: true
    // })
  ])
}