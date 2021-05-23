<template>
<v-skeleton-loader v-if="isLoading"/>
<v-container v-else>
<r-pdf-viewer
							controller="pdfGallery"
							action="getItemPdf"
							:request-body="requestBody"
							:toolbar="toolbar"
							download-file-name="pdfViewer.pdf"
							:key="pdfViewerKey">
</r-pdf-viewer>
<v-snackbar v-model="showSnackbar"
						:timeout="2000">
 {{$t('pdfGallery.snackBarCaption', { item: item.itemPartNumber})}}
</v-snackbar>
<r-action-buttons :actions="actionButtons">
	 <template #spacer><v-spacer /></template>
</r-action-buttons>

</v-container>

</template>
<script lang="ts">

import { Component, Vue } from 'vue-property-decorator';
import { RPdfViewer, IPdfViewerToolbarItem, RActionButtons, IButtonAction } from '@/container';
import { IPostMessage } from './types';
@Component({
	components: { RPdfViewer, RActionButtons }
})
export default class PdfViewer extends Vue {
	toolbar: IPdfViewerToolbarItem[] = [
		'rotate-left', 'rotate-right',
		'spacer',
		'prev-page', 'page-selection', 'next-page',
		'spacer',
		'download'
	];
	opener: Window | null = null;
	isLoading: boolean = true;
	item: any = {};
	requestBody = {
		itemId: 0
	};
	showSnackbar: boolean = false;
	pdfViewerKey: number = 0;
	get actionButtons(): IButtonAction[] {
		return [
			{
				localeKey: 'pdfGallery.previous',
				icon: 'fa-arrow-left',
				onAction: () => this.postMessage({ type: 'arrowPressed', data: { isArrowDown: false } })
			},
			{
				format: 'spacer',
				cols: false
			},
			{
				localeKey: 'pdfGallery.next',
				icon: 'fa-arrow-right',
				onAction: () => this.postMessage({ type: 'arrowPressed', data: { isArrowDown: true } })
			}
		];
	}
	created() {
		this.opener = window.opener;
		document.addEventListener('keydown', this.onKeyDown);
		window.addEventListener('message', this.messageHandler);
	}

	mounted() {
		this.postMessage({ type: 'windowReady', data: { isReady: true } });
	}

	messageHandler(event: MessageEvent) {
		// https://developer.mozilla.org/en-US/docs/Web/API/Window/postMessage
		if (event.origin !== window.location.origin)
			return;
		const message = event.data as IPostMessage;
		if (message.type === 'postItem') {
			this.item = message.data.item;
			this.requestBody.itemId = this.item.id;
			if (this.isLoading)
				this.isLoading = false;
			else
				this.reloadPdf();
			this.showSnackbar = true;
		}
	}

	postMessage(message: IPostMessage) {
		if (this.opener) {
			this.opener.postMessage(message, window.location.href);
		}
	}

	reloadPdf() {
		this.pdfViewerKey++;
		return;
	}

	onKeyDown(event: KeyboardEvent) {
		const keyCode = event.keyCode;
		if (keyCode === 27) {
			window.close();
		}
		if (keyCode === 38 || keyCode === 40) { // arrow Up or arrow Down
			const isArrowDown = keyCode === 40;
			this.postMessage({ type: 'arrowPressed', data: { isArrowDown } });
		}
	}
}
</script>
<route>
{
	includeInDrawer: false,
	layout: "empty"
}
</route>
