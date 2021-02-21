pub use crate::{
    ffi::{FFIArray, FFIStr},
    BOOKMARK,
};
use kataru::*;
use std::os::raw::c_char;

pub static mut STORY: Option<Story> = None;
pub static mut RUNNER: Option<Runner> = None;
pub static mut LINE: Option<&Line> = None;

// For stories.
fn try_load_story(path: &str) -> Result<()> {
    unsafe {
        STORY = Some(Story::load_yml(path)?);
        Ok(())
    }
}
#[no_mangle]
pub extern "C" fn load_story(path: *const c_char, length: usize) -> FFIStr {
    let path = FFIStr::to_str(path, length);
    FFIStr::result(try_load_story(path))
}
fn try_save_story(path: &str) -> Result<()> {
    unsafe {
        match &STORY {
            Some(story) => story.save(path),
            None => Err(error!("Bookmark was None.")),
        }
    }
}
#[no_mangle]
pub extern "C" fn save_story(path: *const c_char, length: usize) -> FFIStr {
    let path = FFIStr::to_str(path, length);
    FFIStr::result(try_save_story(path))
}

// For runner.
fn try_init_runner() -> Result<()> {
    unsafe {
        if let Some(bookmark) = BOOKMARK.as_mut() {
            if let Some(story) = STORY.as_ref() {
                bookmark.init_state(story);
                RUNNER = Some(Runner::new(bookmark, story)?);
                Ok(())
            } else {
                Err(error!("Story was None."))
            }
        } else {
            Err(error!("Bookmark was None."))
        }
    }
}
#[no_mangle]
pub extern "C" fn init_runner() -> FFIStr {
    FFIStr::result(try_init_runner())
}

// For runner.
fn try_validate() -> Result<()> {
    unsafe {
        if let Some(story) = STORY.as_ref() {
            Validator::new(story).validate()?;
            Ok(())
        } else {
            Err(error!("Story was None."))
        }
    }
}
#[no_mangle]
pub extern "C" fn validate() -> FFIStr {
    FFIStr::result(try_validate())
}

fn try_next(input: &str) -> Result<()> {
    unsafe {
        if let Some(runner) = RUNNER.as_mut() {
            LINE = Some(runner.next(input)?);
            Ok(())
        } else {
            Err(error!("Runner was not initialized."))
        }
    }
}
#[no_mangle]
pub extern "C" fn next(input: *const c_char, length: usize) -> FFIStr {
    let input = FFIStr::to_str(input, length);
    FFIStr::result(try_next(&input))
}

#[no_mangle]
pub extern "C" fn tag() -> LineTag {
    unsafe { LineTag::tag(&LINE) }
}
