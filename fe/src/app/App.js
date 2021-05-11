import React, { Suspense, useEffect, useState } from 'react';
import 'antd/dist/antd.css'
import { Layout, Menu, Breadcrumb } from 'antd';
import { UserOutlined, LaptopOutlined, NotificationOutlined } from '@ant-design/icons';
import Routers from './router';
import { BrowserRouter, Link, useHistory } from 'react-router-dom';
import { LocalizeProvider } from 'react-localize-redux';
import store from './store';
import initializeLanguage from './translations';
import "./access/css/index.css"
import router from "../modules/template/router"
const { SubMenu } = Menu;
const { Content } = Layout;
function App() {
  const { pathname } = window.location;
  const [select, setSelect] = useState(pathname)
  function handelMenu(item) {
    setSelect(item.key);
  }
  // useEffect(() => {
  //   setSelect(pathname);
  // }, [pathname])
  return (

    <Layout>
      <BrowserRouter >

        <Layout.Sider
          width={256}
          theme='light'
          breakpoint="lg"
          trigger={null}
          collapsible
          className='sider'
        >
          <div className='brand'>
            <div className='logo'>
              <img alt="logo" src={"https://gw.alipayobjects.com/zos/rmsportal/KDpgvguMpGfqaHPjicRK.svg"} />Smart Tool
            </div>

          </div>

          <div className='menuContainer'>
            <Menu
              mode="inline"
              defaultSelectedKeys={[select]}
              defaultOpenKeys={[select]}
              style={{ height: '100%', borderRight: 0 }}
              onClick={handelMenu}
            >
              {
                (router.filter(i => i.sidebar).map(i => (
                  <Menu.Item key={i.path} icon={<NotificationOutlined />}  ><Link to={i.path}>{i.title}</Link></Menu.Item>
                )))
              }
            </Menu>

          </div>

        </Layout.Sider>
        <Layout style={{ padding: '0 24px 24px', minHeight: "100vh", overflow: 'auto' }}>

          <Content
            className="site-layout-background"
            style={{
              padding: 24,
              margin: 0,
              minHeight: '100%',
            }}
          >
            <LocalizeProvider
              initialize={initializeLanguage}
              store={store}
            >
              <React.Fragment>
                <Suspense fallback={<p>loading ....</p>}>
                  <Routers />
                </Suspense>

              </React.Fragment>
            </LocalizeProvider>
          </Content>
        </Layout>
      </BrowserRouter>

    </Layout>
  );
}

export default App;
