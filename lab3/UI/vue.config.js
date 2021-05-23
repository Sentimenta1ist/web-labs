module.exports = {
	transpileDependencies: [
		/node_modules[/\\\\]@round[/\\\\]/,
		'vuetify'
	],
	productionSourceMap: false,
	publicPath: `${process.env.VUE_APP_FRONTEND_VIRTUAL_APP}`,
  devServer: {
		https: (process.env.VUE_APP_FRONTEND_DEV_SSL_ENABLED === 'true'),
		port: process.env.VUE_APP_FRONTEND_DEV_PORT
  },
	//From here: https://www.mistergoodcat.com/post/the-joy-that-is-source-maps-with-vuejs-and-typescript
	configureWebpack: config => {
		if (process.env.NODE_ENV === 'development') {
			config.devtool = 'eval-source-map';

			config.output.devtoolModuleFilenameTemplate = info => {
				let $filename = `sources://${info.resourcePath}`;

				// if (info.resourcePath.match(/\.vue$/) && !info.identifier.match(/type=script/)) {
				if (info.resourcePath.match(/\.vue$/) && !info.query.match(/type=script/)) {
					$filename = `webpack-generated:///${info.resourcePath}?${info.hash}`;
				}

				return $filename;
			};

			config.output.devtoolFallbackModuleFilenameTemplate = 'webpack:///[resource-path]?[hash]';
		}
		//For resolving export/import of typescript interface warning
		//See below
		config.plugins.unshift(new IgnoreNotFoundExportPlugin());
	},

	chainWebpack: config => {
		config.module
			.rule('route')
			.resourceQuery(/blockType=route/)
			.type('javascript/auto')
			.use('route')
			.loader('./routeLoader.js');
	},

	pluginOptions: {
		i18n: {
			locale: process.env.VUE_APP_DEFAULT_LOCALE,
			fallbackLocale: process.env.VUE_APP_DEFAULT_LOCALE,
			localeDir: 'locales',
			enableInSFC: true
		}
	}
}

// For resolving export/import of typescript interface warning
// https://github.com/TypeStrong/ts-loader/issues/653
// It is unshifted in the webpack plugin list, during configuredwebPack method below.
const ModuleDependencyWarning = require('webpack/lib/ModuleDependencyWarning')
class IgnoreNotFoundExportPlugin {
	apply(compiler) {
		const messageRegExp = /export '.*'( \(reexported as '.*'\))? was not found in/;

		function doneHook(stats) {
			stats.compilation.warnings = stats.compilation.warnings.filter(function (warn) {
				if (warn instanceof ModuleDependencyWarning && messageRegExp.test(warn.message))
					return false

				return true;
			});
		}
		if (compiler.hooks)
			compiler.hooks.done.tap('IgnoreNotFoundExportPlugin', doneHook);
		else
			compiler.plugin('done', doneHook);
	}
}
