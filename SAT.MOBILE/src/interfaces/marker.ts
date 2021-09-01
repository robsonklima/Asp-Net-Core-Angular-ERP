export interface Marker {
	lat: number;
	lng: number;
	label?: string;
	draggable: boolean;
	iconUrl: string;
	title: string;
	subtitle: string;
}