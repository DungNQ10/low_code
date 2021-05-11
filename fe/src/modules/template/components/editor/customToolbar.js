import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListSubheader from '@material-ui/core/ListSubheader';
import TextField from '@material-ui/core/TextField';
import { Input } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import {
  connect,
  ContentPlugin,
  Editor,
  LayoutPlugin,
  Plugin,
  sanitizeInitialChildren,
  Selectors,
  useEditor
} from '@react-page/core';
import * as React from 'react';
import { Portal } from 'react-portal';
import { createStructuredSelector } from 'reselect';
import Item from '@react-page/ui/lib/Toolbar/Item/index';


const defaultTranslations = {
  noPluginFoundContent: 'No plugin found.',
  searchPlaceholder: 'Search',
  layoutPlugins: 'Layout plugins',
  contentPlugins: 'Content plugins',
  insertPlugin: 'Insert plugin',
  dragMe: 'Drag me'
};
class Raw extends React.Component {
  static defaultProps = {
    translations: defaultTranslations,
  };


  constructor(props) {
    super(props);
    this.state = {
      isSearching: false,
      searchText: '',
    };

    this.onSearch = this.onSearch.bind(this);
    this.searchFilter = this.searchFilter.bind(this);
  }

  componentDidUpdate() {
    console.log(this.props);
    const input = this.input;
    if (input && this.props.isInsertMode && input instanceof HTMLElement) {
      setTimeout(() => {
        const e = input.querySelector('input');
        if (e) {
          e.focus();
        }
      }, 100);
    }
  }

  onRef = component => {
    this.input = component;
  }

  onSearch = e => {
    const target = e.target;
    if (target instanceof HTMLInputElement) {
      this.setState({
        isSearching: target.value.length > 0,
        searchText: target.value,
      });
    }
  }

  searchFilter(plugin) {
    let ret =(
      plugin &&
      plugin.name &&
      !plugin.hideInMenu &&
      (plugin.name
        .toLowerCase()
        .startsWith(this.state.searchText.toLowerCase()) ||
        (plugin.description &&
          plugin.description
            .toLowerCase()
            .startsWith(this.state.searchText.toLowerCase())) ||
        (plugin.text &&
          plugin.text
            .toLowerCase()
            .startsWith(this.state.searchText.toLowerCase())))
    );
    return ret;
  }

  render() {
    const {
      editor: { plugins },
    } = this.props;
    const content = plugins.plugins.content.filter(this.searchFilter).filter(c=>c.name!=="EmptyContent");
   
    const layout = plugins.plugins.layout.filter(this.searchFilter);

    return (
      <Portal>
        <Drawer
          variant="persistent"
          className="ory-toolbar-drawer"
          open={this.props.isInsertMode}
          PaperProps={{
            style: {
              width: 320,
            },
          }}
        >
          <List
            className="ory-toolbar-list"
            // subheader={
            //   <ListSubheader>
            //     {this.props.translations.insertPlugin}
            //   </ListSubheader>
            // }
          >
            <ListItem>
              {/* <TextField
                  inputRef={this.onRef}
                  placeholder={this.props.translations.searchPlaceholder}
                  fullWidth={true}
                  onChange={this.onSearch}
                /> */}
              <Input
                className="search-button"
                prefix={<SearchOutlined />}

                size="large"
                placeholder={this.props.translations.searchPlaceholder}
                inputRef={this.onRef}
                onChange={this.onSearch}
              ></Input>
            </ListItem>
            {layout.length + content.length === 0 && (
              <ListSubheader>
                {this.props.translations.noPluginFoundContent}
              </ListSubheader>
            )}
          </List>
          {content.length > 0 && (
            <List
              className="ory-toolbar-list-items"
              subheader={
                <ListSubheader>
                  {this.props.translations.contentPlugins}
                </ListSubheader>
              }
            >
              {content.map((plugin, k) => {
                const initialState = plugin.createInitialState();

                return (
                  <Item
                    translations={this.props.translations}
                    plugin={plugin}
                    key={k.toString()}
                    className="abc"
                    insert={{
                      content: {
                        plugin,
                        state: initialState,
                      },
                    }}
                  />
                );
              })}
            </List>
          )}
          {layout.length > 0 && (
            <List
              subheader={
                <ListSubheader>
                  {this.props.translations.layoutPlugins}
                </ListSubheader>
              }
            >
              {layout.map((plugin, k) => {
                const initialState = plugin.createInitialState();

                const children = sanitizeInitialChildren(
                  plugin.createInitialChildren()
                );

                return (
                  <Item
                    translations={this.props.translations}
                    plugin={plugin}
                    key={k.toString()}
                    insert={{
                      ...children,
                      layout: {
                        plugin,
                        state: initialState,
                      },
                    }}
                  />
                );
              })}
            </List>
          )}
        </Drawer>
      </Portal>
    );
  }
}

const mapStateToProps = createStructuredSelector({
  isInsertMode: Selectors.Display.isInsertMode,
});

const Decorated = connect(mapStateToProps)(Raw);

const Toolbar = () => {
  const editor = useEditor();
  return <Decorated editor={editor} />;
};

export default React.memo(Toolbar);
