<template>
	<div class="home">
	</div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { Mutation, namespace } from 'vuex-class';
import { appBarVuexNamespace, IAppBarTitleModel } from '@/container';

@Component
export default class Home extends Vue {

	@appBarVuexNamespace.Mutation
	setTitle!: (toolbarTitle: IAppBarTitleModel | null) => void;

	mounted() {
		this.setTitle({
			pageContext: this.$t('home').toString()
		});
	}

}
</script>

<route>
{
	"path": "/",
	"icon": "fa-home"
}
</route>
