<template>
	<v-app>

		<v-content>
			<v-container fluid>
				<slot />
			</v-container>
		</v-content>
	</v-app>
</template>

<script lang="ts">

import { Vue, Component, Watch } from 'vue-property-decorator';

@Component({
	components: {}
})
export default class EmptyLayout extends Vue {
}

</script>
