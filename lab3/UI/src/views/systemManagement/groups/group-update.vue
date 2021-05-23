<template>
	<popup-form :title="$t('systemManagement.groupUpdate')"
							:actions="actions">
		<v-container>
			<v-row>
				<v-col cols=12>
					<v-text-field :label="required($t('systemManagement.groups.name'))"
												:error="hasError('name')"
												:error-messages="errorMessages('name')"
												:hide-details="!hasError('name')"
												v-model="model.name"
												outlined dense
												placeholder=" "/>
				</v-col>
				<v-col cols=12>
					<r-combobox v-model="model.idUserList"
											:controller="controller"
											action="userFullNames"
											item-key="idUser"
											item-value="fullName"
											multiselect
											small-chips deletable-chips
											:label="$t('systemManagement.groups.idUserList')"
											:error="hasError('idUserList')"
											:error-messages="errorMessages('idUserList')"
											:hide-details="!hasError('idUserList')"
											outlined dense
											placeholder=" " />
				</v-col>
				<v-col cols=12>
					<r-combobox :selected.sync="model.permissions"
											multiselect
											small-chips deletable-chips
											:items="permissions"
											:label="required($t('systemManagement.groups.permissions'))"
											:error="hasError('permissions')"
											:error-messages="errorMessages('permissions')"
											:hide-details="!hasError('permissions')"
											outlined dense
											placeholder=" " />
				</v-col>
				<v-col class="py-0"><small>{{$t('app.mandatoryMarkerExplanation')}}</small></v-col>
			</v-row>
		</v-container>
	</popup-form>
</template>

<script lang="ts">

import { Vue, Component, Watch, Prop, Model, Mixins } from 'vue-property-decorator';
import { PopupForm } from '@/layouts/default';
import { ValidationMixin, RCombobox, IButtonAction, apiService, configuration, dialogService, DialogMixin } from '@/container';
import { GroupUpdateModel } from './types';

@Component({
	components: { PopupForm, RCombobox }
})
export default class GroupUpdate extends Mixins(ValidationMixin, DialogMixin) {
	@Prop({ required: true }) public model!: GroupUpdateModel;

	private controller = 'system';
	private action = 'group/update';

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
		this.permissions = await apiService.get<string[]>(this.controller, 'permissions');
	}

	private async onSave(action: IButtonAction): Promise<void> {
		this.validateName();
		this.validatePermissions();

		if (this.hasErrors)
			return;

		action.loading = true;

		await this.catchValidationErrorsAsync(() =>
			apiService.post(this.controller, this.action, this.model));

		if (!this.hasErrors)
			this.close(true);
	}

	private validateName() {
		this.removeErrors('name');
		this.require('name', this.model.name);
		this.maxLength('name', this.model.name, this.model.nameMaxLength);
	}

	private validatePermissions() {
		this.removeErrors('permissions');

		if (this.model.permissions.length === 0)
			this.addError('permissions', this.$t('round.error.isRequired').toString());
	}

	@Watch('model.name')
	private onNameChanged() {
		this.validateName();
	}

	@Watch('model.permissions')
	private onPermissionsChanged() {
		this.validatePermissions();
	}
}

</script>
