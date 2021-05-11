import globalTranslations from './global';
import ReactDOMServer from 'react-dom/server';

const initializeLanguage = {
  languages: [
    { name: 'Korean', code: 'ko' },
    { name: 'English', code: 'en' }
  ],
  translation: globalTranslations,
  options: {
    renderToStaticMarkup: ReactDOMServer.renderToStaticMarkup,
    defaultLanguage: 'ko'
  }
};

export default initializeLanguage;
