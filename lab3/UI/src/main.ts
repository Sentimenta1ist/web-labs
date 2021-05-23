import Vue from 'vue';
import App from '@/App.vue';
import store from '@/store';
import routes from '@/routes';
import '@/auth';
import container, { routerService, localizationService} from '@/container';
import vuetify from './plugins/vuetify';

// tslint:disable-next-line:no-console
Vue.prototype.$log = console.log.bind(console);
Vue.config.productionTip = false;

container.init(store, routes);

new Vue({
	router: routerService.router,
	vuetify,
	store,
	i18n: localizationService.i18n,
	render: h => h(App)
}).$mount('#app');
