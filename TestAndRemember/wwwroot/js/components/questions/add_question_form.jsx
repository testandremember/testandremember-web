var AddQuestionForm = React.createClass({
    render: function() {
        return (
          <div className="add-question-form">
            Hello, world! I am a question add form.
          </div>
      );
    }
});
ReactDOM.render(
  <AddQuestionForm />,
  document.getElementById('add-question-form')
);