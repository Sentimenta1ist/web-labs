<template>
<div class="container justify-center mb-6" style="">
<v-row >
	<v-col cols=2 class="justify-center">
		<h1>Your timer</h1>
	</v-col>
</v-row>
<v-row>
	<v-col>
		<h2>TIME</h2>
		<!-- <v-text-field model="timer"></v-text-field> -->
		 <div id="timer">
    <span id="minutes">{{ minutes }}</span>
    <span id="middle">:</span>
    <span id="seconds">{{ seconds }}</span>
  </div>
	</v-col>
</v-row>
<v-row>
	<v-col cols=3>
	<v-text-field label='Name of activity' v-model="result.name"></v-text-field>
	</v-col>
</v-row>
<v-row>
	<v-col>
	 <v-checkbox
      v-model="checkbox"
      label="Save time of begin and end"
    ></v-checkbox>
	</v-col>
</v-row>
<v-row class="justify-start">
	<v-col cols=1>
		<v-btn @click="Start">
			Start
		</v-btn>
	</v-col>
		<v-col cols=1>
		<v-btn @click="pauseTimer">
			Pause
		</v-btn>
	</v-col  cols=1>
		<v-col>
		<v-btn @click="stopTimer">
			Stop
		</v-btn>
	</v-col>
</v-row>
<v-row>
	<v-col>
		<h3>Enter date of choose another option</h3>
	</v-col>
</v-row>
<v-row>
	<v-col cols=3>
		<v-text-field label="Input days ex 01/11-02/11"></v-text-field>
	</v-col>
			<v-col col=1>
		<v-btn @click="showChange">
			Show session
		</v-btn>
	</v-col>
</v-row>
<v-row>
	<v-col>
			<r-action-buttons :actions="actions" ></r-action-buttons>
	</v-col>
</v-row>
<v-row>
	<v-col>
		<h2>Your session</h2>
	</v-col>
</v-row>
<v-row>
<v-col cols=6>
<r-table v-if="show"
				controller="timer"
				action="getTimer"
				:headers="headers"
>
</r-table>
</v-col>
</v-row>


</div>

</template>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { RTable, RActionButtons, IButtonAction, apiService, ITableHeader } from '@/container';
import { IPostMessage } from './types';
@Component({
	components: { RTable, RActionButtons }
})
export default class PdfGallery extends Vue {
	checkbox: boolean = false;
	currentTime: number = 0;
	clockTimer: any;
	totalTime: number = 0;
	timer: any;
	result: IPostMessage = {
		end: undefined,
		begin: undefined,
		name: '',
		time: '00:00'
	};
	showAll: boolean = false;


	headers: ITableHeader[] = [
		{
			field: 'name',
			localeKey: 'Name'
		},
		{
			field: 'time',
			localeKey: 'Time'
		}
	];

	get show() {
		return this.showAll;
	}

	get actions(): IButtonAction[] {
		return [
			{
				text: 'Day',
				onAction: this.onAction
			},
			{
				text: 'Week',
				onAction: this.onAction
			},
			{
				text: 'Month',
				onAction: this.onAction
			},
			{
				text: 'Show all session',
				onAction: this.onAction
			},
			{
				text: 'Get sum time',
				onAction: this.onAction
			}
		];

	}

	onAction() {
	}

	Start() {
		this.timer = setInterval(() => this.countdown(), 1000);
		this.result!.begin = new Date();

	}
	countdown() {
		// if (this.totalTime >= 1) {
		this.unpauseTimer();
		this.totalTime++;
		// } else {
		// 	this.totalTime = 0;
		// 	this.resetTimer()
		// }
	}
	get minutes(): string {
		const minutes = Math.floor(this.totalTime / 60);
		return this.padTime(minutes);
	}
	get seconds() {
		const seconds = this.totalTime - (this.minutes * 60);
		return this.padTime(seconds);
	}

	padTime(time) {
		return (time < 10 ? '0' : '') + time;
	}

	resetTimer() {
		this.totalTime = (0 * 60);
		//clearInterval(this.timer);
		this.timer = null;
	}

	unpauseTimer() {
		this.totalTime = (this.minutes * 60) + this.seconds;
	}

	showChange() {
		if (this.showAll) {
			this.showAll = false;
		}
		else {
			this.showAll = true;
		}
	}

	pauseTimer() {
		clearInterval(this.timer);
		this.timer = `${this.minutes}:${this.seconds}`;
		console.log(this.timer);
	}

	async stopTimer() {
		this.result!.end = new Date();
		clearInterval(this.timer);
		//this.timer = `${this.minutes}:${this.seconds}`;
		await apiService.post('timer', 'saveTime', { time: `${this.minutes}:${this.seconds}`, name: this.result.name });
		this.resetTimer();
	}

	// showSessions(sesList, timer) {
	// 	let dates = document.getElementById("choose").value;
	// 	dates = dates.split("-");
	// 	if (dates.length != 2) {
	// 		alert("Wrong dates!");
	// 	}
	// 	else {
	// 		const date = this.parseDate(dates[0], dates[1]);
	// 		const date1 = date[0];
	// 		const date2 = date[1];
	// 		const daysLag = Math.ceil(Math.abs(date2.getTime() - date1.getTime()) / (1000 * 3600 * 24));
	// 		return this.LastDays(daysLag, date2);;
	// 	}
	// }
}
</script>
<style lang="scss">
#container {
	display: flex;
	flex-direction: column;
	width: 100%;
	justify-content: center;
	align-items: center;
}
</style>
<route>
{
	icon: "fa-file-pdf"
}
</route>
