<template>
	<popup-form :title="$t('systemManagement.userCreate')"
							:actions="actions">
		<v-container>
			<v-row>
				<v-col cols=12>
					<v-text-field :label="required($t('systemManagement.users.username'))"
												:error="hasError('username')"
												:error-messages="errorMessages('username')"
												:hide-details="!hasError('username')"
												v-model="model.username"
												outlined dense
												placeholder=" "/>
				</v-col>
				<v-col cols=6>
					<v-text-field :label="required($t('systemManagement.users.firstName'))"
												:error="hasError('firstName')"
												:error-messages="errorMessages('firstName')"
												:hide-details="!hasError('firstName')"
												v-model="model.firstName"
												outlined dense
												placeholder=" "/>
				</v-col>
				<v-col cols=6>
					<v-text-field :label="required($t('systemManagement.users.lastName'))"
												:error="hasError('lastName')"
												:error-messages="errorMessages('lastName')"
												:hide-details="!hasError('lastName')"
												v-model="model.lastName"
												outlined dense
												placeholder=" "/>
				</v-col>
				<v-col cols=12>
					<v-text-field :label="required($t('systemManagement.users.eMail'))"
												:error="hasError('email')"
												:error-messages="errorMessages('email')"
												:hide-details="!hasError('email')"
												v-model="model.email"
												outlined dense
												placeholder=" "/>
				</v-col>
				<v-col cols=6>
					<r-combobox :label="required($t('systemManagement.users.languageCode'))"
											:error="hasError('languageCode')"
											:error-messages="errorMessages('languageCode')"
											:hide-details="!hasError('languageCode')"
											v-model="model.languageCode"
											:items="languages"
											outlined dense
											placeholder=" "/>
				</v-col>
				<v-col cols=6>
					<r-combobox :label="required($t('systemManagement.users.cultureCode'))"
											:error="hasError('cultureCode')"
											:error-messages="errorMessages('cultureCode')"
											:hide-details="!hasError('cultureCode')"
											v-model="model.cultureCode"
											:items="cultures"
											outlined dense
											placeholder=" "/>
				</v-col>
				<v-col cols=12>
					<v-text-field :label="$t('systemManagement.users.telephoneNumber')"
												:error="hasError('telephoneNumber')"
												:error-messages="errorMessages('telephoneNumber')"
												:hide-details="!hasError('telephoneNumber')"
												v-model="model.telephoneNumber"
												outlined dense
												placeholder=" "/>
				</v-col>
				<v-col cols=12>
					<r-combobox v-model="model.idGroupList"
											:controller="controller"
											action="groupNames"
											item-key="idGroup"
											item-value="name"
											multiselect
											small-chips deletable-chips
											:label="$t('systemManagement.users.idGroupList')"
											:error="hasError('idGroupList')"
											:error-messages="errorMessages('idGroupList')"
											:hide-details="!hasError('idGroupList')"
											outlined dense
											placeholder=" "/>
				</v-col>
				<v-col cols=12>
					<r-combobox :selected.sync="model.permissions"
											multiselect
											small-chips deletable-chips
											:items="permissions"
											:label="$t('systemManagement.users.permissions')"
											:error="hasError('permissions')"
											:error-messages="errorMessages('permissions')"
											:hide-details="!hasError('permissions')"
											outlined dense
											placeholder=" "/>
				</v-col>
				<v-col class="py-0"><small>{{$t('app.mandatoryMarkerExplanation')}}</small></v-col>
			</v-row>
		</v-container>
	</popup-form>
</template>

<script lang="ts">

import { Vue, Component, Watch, Model, Mixins } from 'vue-property-decorator';
import { PopupForm } from '@/layouts/default';
import { ValidationMixin, RCombobox, IButtonAction, apiService, configuration, dialogService, DialogMixin } from '@/container';
import { UserCreateModel, GroupModel, Validation } from './types';

@Component({
	components: { PopupForm, RCombobox }
})
export default class UserCreate extends Mixins(ValidationMixin, DialogMixin) {
	private controller = 'system';
	private action = 'user/create';
	private model = new UserCreateModel();

	private usernameMaxLength = Validation.usernameMaxLength;
	private firstNameMaxLength = Validation.firstNameMaxLength;
	private lastNameMaxLength = Validation.lastNameMaxLength;
	private emailMaxLength = Validation.emailMaxLength;
	private telephoneNumberMaxLength = Validation.telephoneNumberMaxLength;

	private languages?: any[] = configuration.supportedLanguages.split(',');

	private cultures?: any[] = configuration.supportedCultures.split(',');

	private permissions: string[] = [];

	private get actions(): IButtonAction[] {
		return [{
			localeKey: 'app.save',
			onAction: action => this.onSave(action),
			disabled: this.hasErrors
		}];
	}

	private async created(): Promise<void> {
		this.permissions = await apiService.get(this.controller, 'permissions');
	}

	private async onSave(action: IButtonAction): Promise<void> {
		this.validateUsername();
		this.validateFirstName();
		this.validateLastName();
		this.validateEmail();
		this.validateLanguageCode();
		this.validateCultureCode();

		if (this.hasErrors)
			return;

		action.loading = true;

		await this.catchValidationErrorsAsync(() =>
			apiService.post(this.controller, this.action, this.model));

		if (!this.hasErrors)
			this.close(true);
	}

	private validateUsername() {
		this.removeErrors('username');
		this.require('username', this.model.username);
		this.maxLength('username', this.model.username, this.usernameMaxLength);
	}

	private validateFirstName() {
		this.removeErrors('firstName');
		this.require('firstName', this.model.firstName);
		this.maxLength('firstName', this.model.firstName, this.firstNameMaxLength);
	}

	private validateLastName() {
		this.removeErrors('lastName');
		this.require('lastName', this.model.lastName);
		this.maxLength('lastName', this.model.lastName, this.lastNameMaxLength);
	}

	private validateEmail() {
		this.removeErrors('email');
		this.require('email', this.model.email);
		this.maxLength('email', this.model.email, this.emailMaxLength);
	}

	private validateLanguageCode() {
		this.removeErrors('languageCode');
		this.require('languageCode', this.model.languageCode);
	}

	private validateCultureCode() {
		this.removeErrors('cultureCode');
		this.require('cultureCode', this.model.cultureCode);
	}

	private validateTelephoneNumber() {
		this.removeErrors('telephoneNumber');
		this.maxLength('telephoneNumber', this.model.telephoneNumber, this.telephoneNumberMaxLength);
	}

	@Watch('model.username')
	private onUsernameChanged() {
		this.validateUsername();
	}

	@Watch('model.firstName')
	private onFirstNameChanged() {
		this.validateFirstName();
	}

	@Watch('model.lastName')
	private onLastNameChanged() {
		this.validateLastName();
	}

	@Watch('model.email')
	private onEmailChanged() {
		this.validateEmail();
	}

	@Watch('model.languageCode')
	private onLanguageChanged() {
		this.validateLanguageCode();
	}

	@Watch('model.cultureCode')
	private onCultureCodeChanged() {
		this.validateCultureCode();
	}

	@Watch('model.telephoneNumber')
	private onTelephoneNumberChanged() {
		this.validateTelephoneNumber();
	}
}

</script>
