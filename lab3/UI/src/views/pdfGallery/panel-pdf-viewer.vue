<template>
 <v-card flat>
    <v-card-title v-if="title">
      <slot name="title">
        <span :class="titleClass" v-text="title" />
      </slot>
    </v-card-title>
		<v-card-text>
		<r-pdf-viewer
							controller="pdfGallery"
							action="getItemPdf"
							:request-body="requestBody"
							:toolbar="toolbar"
							download-file-name="pdfViewer.pdf"
							:key="pdfViewerKey">
</r-pdf-viewer>
		</v-card-text>
    <v-card-actions>
			<r-action-buttons :actions="actionButtons">
	 <template #spacer><v-spacer /></template>
</r-action-buttons>
    </v-card-actions>
  </v-card>
</template>
<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { IPdfViewerToolbarItem, RPdfViewer, RActionButtons, IButtonAction } from '@/container';
import { component } from 'vue/types/umd';
@Component({
	components: { RPdfViewer, RActionButtons }
})
export default class PanelPdfOpener extends Vue {
	@Prop({ type: Object, required: true }) item!: any;
	title: string = 'Panel';
	titleClass: string = 'headline';
	internalItem: any = null;
	toolbar: IPdfViewerToolbarItem[] = [
		'rotate-left', 'rotate-right',
		'spacer',
		'prev-page', 'page-selection', 'next-page',
		'spacer',
		'download'
	];
	requestBody = {
		itemId: 0
	};
	pdfViewerKey: number = 0;
	get actionButtons(): IButtonAction[] {
		return [
			{
				localeKey: 'pdfGallery.close',
				icon: 'fa-times',
				onAction: this.onCloseClick
			},
			{
				format: 'spacer',
				cols: false
			},
			{
				localeKey: 'pdfGallery.previous',
				icon: 'fa-arrow-left',
				onAction: () => _.sendMessage('move-previous', { isArrowDown: false })
			},
			{
				localeKey: 'pdfGallery.next',
				icon: 'fa-arrow-right',
				onAction: () => _.sendMessage('move-next', { isArrowDown: true })
			}
		];
	}
	created() {
		this.$watch(() => this.item, this.onItemChange, { immediate: true });
		_.subscribeMessage('item-update', this.onItemUpdate);
	}

	beforeDestroy() {
		_.unsubscribeMessage('item-update', this.onItemUpdate);
	}

	reloadPdf() {
		this.pdfViewerKey++;
		return;
	}

	onCloseClick() {
		_.sendMessage('pdf-viewer-panel-closing');
		this.$emit('close');
	}

	onItemUpdate(item) {
		this.internalItem = item[0].item;
		this.requestBody.itemId = this.internalItem.id;
		this.reloadPdf();
	}

	onItemChange() {
		this.requestBody.itemId = this.item.id;
		this.internalItem = this.item;
	}
}
</script>
