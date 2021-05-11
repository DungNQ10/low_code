import { PageMode } from './store/constant';
import DynamicList from './components/templatePage/dynamicList';
import DynamicFormList from './components/templatePage';
import TemplatePage from "./components/templatePage";
import FormTemp from "./components/templatePage/form";


const router = [
    
    {
        title: "Detail",
        component: TemplatePage,
        props: { globalStore: { pageMode: PageMode.TemplatePage, siteMode: 'Admin' } },
        path: "/template/:id"
    },
    {
        title: "QL Form",
        component: TemplatePage,
        props: { globalStore: { pageMode: PageMode.PreviewPage, siteMode: 'Preview' } },
        path: "/template/preview/:id"
    },
    {
        title: "QL List",
        component: DynamicList,
        props: { globalStore: { pageMode: PageMode.TemplatePage, siteMode: 'Admin' } },
        path: "/template/list/dynamiclist",
        sidebar: true
    },
    {
        title: "QL Form",
        component: DynamicFormList,
        props: { globalStore: { pageMode: PageMode.TemplatePage, siteMode: 'Admin' } },
        path: "/template/form/dynamicform",
        sidebar: true

    },
    {
        title: "QL Form New",
        component: FormTemp,
        props: { globalStore: { pageMode: PageMode.TemplatePage, siteMode: 'Admin' } },
        path: "/template/form/new",
        sidebar: true

    },
    {
        title: "Admin",
        component: TemplatePage,
        props: { globalStore: { pageMode: PageMode.TemplatePage, siteMode: 'Admin' } },
        path: "/template",
        sidebar: true
    }
]

export default router;