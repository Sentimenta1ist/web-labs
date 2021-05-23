import { authorizationService, localizationService, langVuexNamespace, authorizationVuexNamespace, dateTimeService, numberService } from '@/container';
import { Component, Vue, Watch } from 'vue-property-decorator';
import { RouteConfig, Route } from 'vue-router';
import AuthFlowMixin from './authFlow';

@Component
export default class WinAuthFlowMixin extends AuthFlowMixin {

	@authorizationVuexNamespace.State
	currentUser!: any;
	@authorizationVuexNamespace.State
	loadingUserFailure!: any;

	@langVuexNamespace.Getter
	isGlobalizationDefined!: boolean;
	@langVuexNamespace.State
	loadingLocaleFailure!: boolean;

	created() {
		Vue.prototype.$authFlow = { supportLogout: false };
	}

	get authComplete() {
		return _.hasValue(this.currentUser) && this.isGlobalizationDefined;
	}

	@Watch('currentRoute')
	async routeChangeForWinAuthFlow(route: RouteConfig) {
		if (route.name === 'login')
			this.$router.push('/');

		if (!this.authComplete) {
			this.appLoading = true;
			await authorizationService.loadUserInformation();
		} else {
			if (!this.redirectToForbiddenIfNeeded(route))
				this.appLoading = false;
		}
	}

	@Watch('currentUser')
	async userChanged(user: any) {
		await localizationService.loadLocale(user.languageCode, user.cultureCode);
		dateTimeService.changeCulture(user.cultureCode);
		numberService.changeCulture(user.cultureCode);
	}

	@Watch('loadingUserFailure')
	onLoadingUserFailure(loadingUserFailure: any) {
		if (loadingUserFailure) {
			const response = (loadingUserFailure.response && loadingUserFailure.response.status) ? loadingUserFailure.response.status : undefined;
			if (!response || (response && response !== 401 && response !== 403))
				this.appLoadingError = true;
		}
	}

	@Watch('isGlobalizationDefined')
	onIsGlobalizationDefinedChanged(isGlobalizationDefined: boolean) {
		if (isGlobalizationDefined) {
			if (!this.redirectToForbiddenIfNeeded(this.currentRoute))
				this.appLoading = false;
		}
	}

	@Watch('loadingLocaleFailure')
	onLoadingLocaleFailure(loadingLocaleFailure: any) {
		if (loadingLocaleFailure) {
			this.appLoadingError = true;
		}
	}

}
