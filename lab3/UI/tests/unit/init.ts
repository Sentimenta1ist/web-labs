import Vue, { VueConstructor } from 'vue';
import { CombinedVueInstance } from 'vue/types/vue';
import { mount, Wrapper } from '@vue/test-utils';
import chai from 'chai';
import chaiAsPromised from 'chai-as-promised';
import Stub from './stub.vue';
chai.use(chaiAsPromised);

export function mountMixin<T extends Vue>(
	mixin: VueConstructor<T>,
	data?: any
): Wrapper<CombinedVueInstance<T, any, any, any, any>> {
	return mount(Stub, { ...{ mixins: [mixin] }, ...(data || {}) });
}
