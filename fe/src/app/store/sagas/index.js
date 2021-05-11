import { all, takeEvery, takeLeading } from 'redux-saga/effects';
import { moduleSagas } from '../../modules';

/**
 * Make a root saga
 *
 * @returns {object} - Generator functions all combined in one saga
 */
export default function* rootSaga() {
  yield all([
    ...moduleSagas
  ]);
}
