const DATEPICKER_CONFIGS = {
    shortcuts: {
        today: "Today",
        nextWeek: {
            text: "Next Week",
            period: {
                start: (() => {
                    const now = new Date();
                    const nextSaturday = new Date(now);
                    nextSaturday.setDate(now.getDate() + (1 - now.getDay() % 7) + 7);
                    return nextSaturday;
                })(),
                end: (() => {
                    const now = new Date();
                    const nextSunday = new Date(now);
                    nextSunday.setDate(now.getDate() + (7 - now.getDay() % 7) + 7);
                    return nextSunday;
                })()
            }
        },
        nextMonth: {
            text: "Next Month",
            period: {
                start: (() => {
                    const now = new Date();
                    return new Date(now.getFullYear(), now.getMonth() + 1, 1);
                })(),
                end: (() => {
                    const now = new Date();
                    return new Date(now.getFullYear(), now.getMonth() + 2, 0);
                })()
            }
        },
        nextWeekend: {
            text: "Next Weekend",
            period: {
                start: (() => {
                    const now = new Date();
                    const nextSaturday = new Date(now);
                    nextSaturday.setDate(now.getDate() + (6 - now.getDay() + 7) % 7);
                    return nextSaturday;
                })(),
                end: (() => {
                    const now = new Date();
                    const nextSunday = new Date(now);
                    nextSunday.setDate(now.getDate() + (7 - now.getDay() + 7) % 7);
                    return nextSunday;
                })()
            }
        }
    }
};

export default DATEPICKER_CONFIGS;