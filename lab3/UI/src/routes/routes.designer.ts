const routes = [
	{
		name: 'home',
		path: '/',
		component: () => import('@/views/Home.vue'),
		meta: {
			icon: 'fa-home'
		}
	},
	{
		name: 'forbidden',
		path: '/403',
		component: () => import('@/views/Forbidden.vue'),
		meta: {
			layout: 'none',
			includeInDrawer: false
		}
	},
	{
		name: 'notFound',
		path: '/404',
		component: () => import('@/views/NotFound.vue'),
		meta: {
			layout: 'none',
			includeInDrawer: false
		}
	},
	{
		name: 'login',
		path: '/login',
		component: () => import('@/views/Login.vue'),
		meta: {
			layout: 'none',
			includeInDrawer: false
		}
	},
	{
		name: 'pdfGallery',
		path: '/pdfGallery',
		component: () => import('@/views/pdfGallery/PdfGallery.vue'),
		meta: {
			icon: 'fa-file-pdf'
		}
	},
	{
		name: 'pdfGallery.pdfViewer',
		path: '/pdfGallery/pdfViewer',
		component: () => import('@/views/pdfGallery/PdfViewer.vue'),
		meta: {
			includeInDrawer: false,
			layout: 'empty'
		}
	},
	{
		name: 'queryViewer',
		path: '/queryViewer',
		component: () => import('@/views/QueryViewer.vue'),
		meta: {
			layout: 'none',
			includeInDrawer: false,
			visibleTo: [
				'Round_CanProfile'
			]
		}
	},
	{
		name: 'systemManagement',
		path: '/systemManagement',
		redirect: { name: 'systemManagement.users' },
		meta: {
			icon: 'fa-user',
			includeInDrawer: true
		}
	},
	{
		name: 'systemManagement.groups',
		path: '/systemManagement/groups',
		component: () => import('@/views/systemManagement/groups/Groups.vue')
	},
	{
		name: 'systemManagement.users',
		path: '/systemManagement/users',
		component: () => import('@/views/systemManagement/users/Users.vue')
	}
];

export default routes;
