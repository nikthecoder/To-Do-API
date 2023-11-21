const api = 'http://localhost:64440';

const addForm = document.querySelector('#add-form');
const addInput = document.querySelector('#add-input');
const todoList = document.querySelector('#todo-list');
const infoRow = document.querySelector('#info-row');
const itemsLeft = document.querySelector('#items-left');
const showAllButton = document.querySelector('#show-all-button');
const showActiveButton = document.querySelector('#show-active-button');
const showCompletedButton = document.querySelector('#show-completed-button');
const clearCompletedButton = document.querySelector('#clear-completed-button');

// null: all notes should be shown
// true: only completed notes should be shown
// false: only non-completed ("active") notes should be shown
let completed = null;

function start() {
    loadNotes();
}

async function loadNotes() {
    let url = api + '/notes';
    if (completed === true) {
        url += '?completed=true';
    }
    else if (completed === false) {
        url += '?completed=false';
    }

    const response = await fetch(url);
    const notes = await response.json();

    todoList.replaceChildren();
    for (const note of notes) {
        showNote(note)
    }

    // If there are no notes.
    if (notes.length > 0) {
        infoRow.hidden = false;
    }

    // Show the number of notes.
    const remainingResponse = await fetch(api + '/remaining');
    const remaining = await remainingResponse.json();
    const noun = remaining === 1 ? 'item' : 'items';
    itemsLeft.textContent = remaining + ' ' + noun + ' left';
}

function showNote(note) {
    const template = document.querySelector('#todo-item-template');
    const li = template.content.firstElementChild.cloneNode(true);

    const checkbox = li.querySelector('.note-checkbox');
    checkbox.checked = note.isDone;
    checkbox.onchange = async () => {
        await fetch(api + '/notes/' + note.id, {
            method: 'PUT',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                id: note.id,
                text: note.text,
                isDone: checkbox.checked,
            }),
        });
        loadNotes();
    };

    const span = li.querySelector('.note-text');
    span.textContent = note.text;

    const deleteButton = li.querySelector('.delete-button');
    deleteButton.onclick = async () => {
        await fetch(api + '/notes/' + note.id, {method: 'DELETE'});
        loadNotes();
    };

    todoList.append(li);
}

addForm.onsubmit = async event => {
    event.preventDefault();

    const noteText = addInput.value;
    if (noteText.trim() !== '') {
        // Clear the input after adding a note.
        addInput.value = '';
        const note = {
            text: noteText,
            isDone: false,
        };
        await fetch(api + '/notes', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(note),
        });
        loadNotes();
    }
};

showAllButton.onclick = () => {
    completed = null;
    showAllButton.classList.add('selected');
    showActiveButton.classList.remove('selected');
    showCompletedButton.classList.remove('selected');
    loadNotes();
};

showActiveButton.onclick = () => {
    completed = false;
    showAllButton.classList.remove('selected');
    showActiveButton.classList.add('selected');
    showCompletedButton.classList.remove('selected');
    loadNotes();
};

showCompletedButton.onclick = () => {
    completed = true;
    showAllButton.classList.remove('selected');
    showActiveButton.classList.remove('selected');
    showCompletedButton.classList.add('selected');
    loadNotes();
};

clearCompletedButton.onclick = async () => {
    await fetch(api + '/clear-completed', {method: 'POST'});
    loadNotes();
};

start();