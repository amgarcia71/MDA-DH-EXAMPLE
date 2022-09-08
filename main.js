
const ddlGenerator = require('./ddl-generator')


/**
 * Command Handler for DDL Generation
 *
 * @param {Element} base
 * @param {string} path
 * @param {Object} options
 */
function _handleGenerate (base, path, options) {

  // If base is not assigned, popup ElementPicker
  if (!base) {
    app.elementPickerDialog.showDialog('Select a data model to generate MDA', null, type.ERDDataModel).then(function ({buttonId, returnValue}) {
      if (buttonId === 'ok') {
        base = returnValue
        // If path is not assigned, popup Save Dialog to save a file
        if (!path) {
          var file = app.dialogs.showSaveDialog('Save MDA As...', null, null)
          if (file && file.length > 0) {
            path = file
            ddlGenerator.generate(base, path, options)
          }
        } else {
          ddlGenerator.generate(base, path, options)
        }
      }
    })
  } else {
    // If path is not assigned, popup Save Dialog to save a file
    if (!path) {
      var file = app.dialogs.showSaveDialog('Save DDL As...', null, null)
      if (file && file.length > 0) {
        path = file
        ddlGenerator.generate(base, path, options)
      }
    } else {
      ddlGenerator.generate(base, path, options)
    }
  }
}

/**
* Popup PreferenceDialog with DDL Preference Schema
*/
function _handleConfigure () {
  app.commands.execute('application:preferences', 'mda')
}

function init () {
  app.commands.register('mda:generate', _handleGenerate)
  app.commands.register('mda:configure', _handleConfigure)
}

exports.init = init
